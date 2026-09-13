using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kopator.App.Services;
using Kopator.Core;
using Kopator.Core.Services;
using Kopator.Core.Settings;

namespace Kopator.App.ViewModels;

/// <summary>Flattens a folder tree in place.</summary>
public sealed partial class CollectViewModel(IDialogService dialogs) : OperationViewModel(dialogs)
{
    private readonly CollectService _service = new();

    [ObservableProperty]
    public partial string Path { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Ignore { get; set; } = string.Empty;

    public override KopatorMode Mode => KopatorMode.Collect;

    public override string Header => "Sammeln";

    public override string ActionLabel => "Sammeln";

    public override string CompletedMessage => "Sammelvorgang abgeschlossen!";

    public override string CancelledMessage => "Sammelvorgang abgebrochen!";

    public override int CountItems() => CollectService.GetFilesToCollect(Path, Ignore).Count;

    public override Task<OperationResult> ExecuteAsync(IProgress<int> progress, CancellationToken cancellationToken) =>
        Task.Run(() => _service.Execute(new CollectRequest(Path, Ignore), progress, cancellationToken));

    public override void LoadFrom(KopatorSettings settings)
    {
        Path = settings.CollectPath;
        Ignore = settings.CollectIgnore;
    }

    public override void SaveTo(KopatorSettings settings)
    {
        settings.CollectPath = Path;
        settings.CollectIgnore = Ignore;
    }

    public override string DescribeError(ValidationError error) => error switch
    {
        ValidationError.MissingPath => "Bitte einen Sammelordner angeben!",
        _ => base.DescribeError(error),
    };

    [RelayCommand]
    private async Task BrowsePathAsync() =>
        Path = await PickFolderAsync("Sammelziel auswählen.", Path) ?? Path;
}
