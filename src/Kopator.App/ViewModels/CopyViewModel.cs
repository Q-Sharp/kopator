using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kopator.App.Services;
using Kopator.Core;
using Kopator.Core.Services;
using Kopator.Core.Settings;

namespace Kopator.App.ViewModels;

/// <summary>Copies or moves the files of one folder into another.</summary>
public sealed partial class CopyViewModel(IDialogService dialogs) : OperationViewModel(dialogs)
{
    private readonly CopyService _service = new();

    [ObservableProperty]
    public partial string Source { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Destination { get; set; } = string.Empty;

    /// <summary>Set from the window's "Verschieben?" checkbox, which only applies to this tab.</summary>
    public bool Move { get; set; }

    public override KopatorMode Mode => KopatorMode.Copy;

    public override string Header => "Kopieren";

    public override string ActionLabel => Move ? "Verschieben" : "Kopieren";

    public override string CompletedMessage => "Kopiervorgang abgeschlossen!";

    public override string CancelledMessage => "Kopiervorgang abgebrochen!";

    public override int CountItems() => CopyService.CountFiles(Source);

    public override Task<OperationResult> ExecuteAsync(IProgress<int> progress, CancellationToken cancellationToken) =>
        Task.Run(() => _service.Execute(new CopyRequest(Source, Destination, Move), progress, cancellationToken));

    public override void LoadFrom(KopatorSettings settings)
    {
        Source = settings.CopySource;
        Destination = settings.CopyDestination;
    }

    public override void SaveTo(KopatorSettings settings)
    {
        settings.CopySource = Source;
        settings.CopyDestination = Destination;
    }

    [RelayCommand]
    private async Task BrowseSourceAsync() =>
        Source = await PickFolderAsync("Quelle auswählen.", Source) ?? Source;

    [RelayCommand]
    private async Task BrowseDestinationAsync() =>
        Destination = await PickFolderAsync("Ziel auswählen.", Destination) ?? Destination;
}
