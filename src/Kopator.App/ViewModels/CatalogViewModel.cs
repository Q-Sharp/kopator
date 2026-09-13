using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kopator.App.Services;
using Kopator.Core;
using Kopator.Core.Services;
using Kopator.Core.Settings;

namespace Kopator.App.ViewModels;

/// <summary>Writes a listing of a folder tree as CSV or HTML.</summary>
public sealed partial class CatalogViewModel(IDialogService dialogs) : OperationViewModel(dialogs)
{
    private readonly CatalogService _service = new(new SkiaThumbnailProvider());

    [ObservableProperty]
    public partial string Source { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string DestinationFile { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string FileType { get; set; } = string.Empty;

    [ObservableProperty]
    public partial CatalogExportType ExportType { get; set; } = CatalogExportType.Csv;

    /// <summary>Bound to the export type combo box.</summary>
    public IReadOnlyList<CatalogExportType> ExportTypes { get; } = Enum.GetValues<CatalogExportType>();

    public override KopatorMode Mode => KopatorMode.Catalog;

    public override string Header => "Katalogisieren";

    public override string ActionLabel => "Katalogisieren";

    public override string CompletedMessage => "Katalogisiervorgang abgeschlossen!";

    public override string CancelledMessage => "Katalogisiervorgang abgebrochen!";

    public override int CountItems() => CatalogService.CountFiles(Source, FileType);

    public override Task<OperationResult> ExecuteAsync(IProgress<int> progress, CancellationToken cancellationToken) =>
        Task.Run(
            () => _service.Execute(
                new CatalogRequest(Source, DestinationFile, FileType, ExportType), progress, cancellationToken));

    public override void LoadFrom(KopatorSettings settings)
    {
        Source = settings.CatalogSource;
        DestinationFile = settings.CatalogDestinationFile;
        FileType = settings.CatalogFileType;
        ExportType = settings.CatalogExportType;
    }

    public override void SaveTo(KopatorSettings settings)
    {
        settings.CatalogSource = Source;
        settings.CatalogDestinationFile = DestinationFile;
        settings.CatalogFileType = FileType;
        settings.CatalogExportType = ExportType;
    }

    public override string DescribeError(ValidationError error) => error switch
    {
        ValidationError.MissingPath => "Bitte Quellordner und Zieldatei angeben!",
        ValidationError.SourceNotAccessible => "Keine Leserechte für den Quellordner!",
        ValidationError.DestinationNotAccessible => "Die Zieldatei konnte nicht geschrieben werden!",
        _ => base.DescribeError(error),
    };

    [RelayCommand]
    private async Task BrowseSourceAsync() =>
        Source = await PickFolderAsync("Quelle auswählen.", Source) ?? Source;

    [RelayCommand]
    private async Task BrowseDestinationFileAsync()
    {
        var extension = ExportType.ToString().ToLowerInvariant();

        DestinationFile = await Dialogs.PickSaveFileAsync(
            "Zieldatei auswählen.", "export", extension, DestinationFile) ?? DestinationFile;
    }
}
