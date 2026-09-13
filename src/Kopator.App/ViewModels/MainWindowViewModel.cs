using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kopator.App.Services;
using Kopator.Core;
using Kopator.Core.Settings;

namespace Kopator.App.ViewModels;

/// <summary>
/// Owns the window state that is shared across tabs: the move checkbox, the progress bar,
/// the action button and the settings round-trip.
/// </summary>
public sealed partial class MainWindowViewModel : ViewModelBase
{
    private readonly ISettingsStore _settingsStore;
    private readonly IDialogService _dialogs;

    private CancellationTokenSource? _cancellation;

    public MainWindowViewModel(ISettingsStore settingsStore, IDialogService dialogs)
    {
        _settingsStore = settingsStore;
        _dialogs = dialogs;

        Tabs =
        [
            new CopyViewModel(dialogs),
            new CollectViewModel(dialogs),
            new CatalogViewModel(dialogs),
        ];

        var settings = settingsStore.Load();

        foreach (var tab in Tabs)
            tab.LoadFrom(settings);

        Move = settings.Move;
        SelectedTab = Tabs.FirstOrDefault(t => t.Mode == settings.Mode) ?? Tabs[0];
    }

    /// <summary>Raised when an operation finished successfully and the window should close.</summary>
    public event EventHandler? CloseRequested;

    public IReadOnlyList<OperationViewModel> Tabs { get; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActionLabel))]
    [NotifyPropertyChangedFor(nameof(IsMoveEnabled))]
    public partial OperationViewModel SelectedTab { get; set; }

    /// <summary>State of the "Verschieben?" checkbox; only meaningful on the copy tab.</summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActionLabel))]
    public partial bool Move { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActionLabel))]
    [NotifyPropertyChangedFor(nameof(IsIdle))]
    [NotifyPropertyChangedFor(nameof(IsMoveEnabled))]
    public partial bool IsProcessing { get; set; }

    [ObservableProperty]
    public partial int Progress { get; set; }

    [ObservableProperty]
    public partial int ProgressMaximum { get; set; } = 1;

    /// <summary>The action button doubles as the stop button while an operation runs.</summary>
    public string ActionLabel => IsProcessing ? "Stop" : SelectedTab.ActionLabel;

    public bool IsIdle => !IsProcessing;

    /// <summary>Moving is a copy-tab concept; collecting always moves and cataloguing never does.</summary>
    public bool IsMoveEnabled => !IsProcessing && SelectedTab.Mode == KopatorMode.Copy;

    partial void OnSelectedTabChanged(OperationViewModel value) => ApplyMoveToCopyTab();

    partial void OnMoveChanged(bool value) => ApplyMoveToCopyTab();

    // AllowConcurrentExecutions keeps CanExecute true while the task runs. Without it the
    // generated command disables the button, and the "Stop" it turns into would be dead.
    [RelayCommand(AllowConcurrentExecutions = true)]
    private async Task ExecuteAsync()
    {
        // While an operation runs the same button cancels it.
        if (IsProcessing)
        {
            await _cancellation!.CancelAsync();
            return;
        }

        var tab = SelectedTab;

        using var cancellation = new CancellationTokenSource();
        _cancellation = cancellation;

        IsProcessing = true;
        Progress = 0;

        // A zero maximum would render as a full bar, so an empty run shows an empty one.
        ProgressMaximum = Math.Max(1, tab.CountItems());

        try
        {
            // Progress<T> is created here, on the UI thread, so its callbacks marshal back.
            var progress = new Progress<int>(value => Progress = value);
            var result = await tab.ExecuteAsync(progress, cancellation.Token);

            await ReportAsync(tab, result);
        }
        catch (Exception exception)
        {
            await _dialogs.ShowMessageAsync("Fehler", exception.Message);
        }
        finally
        {
            IsProcessing = false;
            _cancellation = null;
            Progress = 0;
        }
    }

    private async Task ReportAsync(OperationViewModel tab, OperationResult result)
    {
        switch (result.Outcome)
        {
            case OperationOutcome.Invalid:
                await _dialogs.ShowMessageAsync("Hinweis", tab.DescribeError(result.Error!.Value));
                break;

            case OperationOutcome.Cancelled:
                await _dialogs.ShowMessageAsync("Abgebrochen", tab.CancelledMessage);
                break;

            default:
                await _dialogs.ShowMessageAsync("Fertig", Describe(tab, result));
                CloseRequested?.Invoke(this, EventArgs.Empty);
                break;
        }
    }

    private static string Describe(OperationViewModel tab, OperationResult result) =>
        result.FailedCount == 0
            ? tab.CompletedMessage
            : $"{tab.CompletedMessage}\n{result.FailedCount} von {result.ProcessedCount + result.FailedCount} Dateien wurden übersprungen.";

    [RelayCommand]
    private void Close() => CloseRequested?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Answers whether the window may close. A running operation blocks it, matching the
    /// behaviour of the original application.
    /// </summary>
    public bool CanClose() => !IsProcessing;

    public void SaveSettings()
    {
        var settings = new KopatorSettings
        {
            Mode = SelectedTab.Mode,
            Move = Move,
        };

        foreach (var tab in Tabs)
            tab.SaveTo(settings);

        _settingsStore.Save(settings);
    }

    private void ApplyMoveToCopyTab()
    {
        foreach (var copy in Tabs.OfType<CopyViewModel>())
            copy.Move = Move;

        OnPropertyChanged(nameof(ActionLabel));
    }
}
