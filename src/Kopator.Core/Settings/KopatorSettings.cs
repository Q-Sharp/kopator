namespace Kopator.Core.Settings;

/// <summary>Everything the application remembers between runs.</summary>
public sealed class KopatorSettings
{
    /// <summary>Tab that was open when the window was last closed.</summary>
    public KopatorMode Mode { get; set; } = KopatorMode.Copy;

    /// <summary>State of the "Verschieben?" checkbox.</summary>
    public bool Move { get; set; }

    public string CopySource { get; set; } = string.Empty;

    public string CopyDestination { get; set; } = string.Empty;

    public string CollectPath { get; set; } = string.Empty;

    /// <summary>Comma-separated globs excluded from a collect run.</summary>
    public string CollectIgnore { get; set; } = string.Empty;

    public string CatalogSource { get; set; } = string.Empty;

    public string CatalogDestinationFile { get; set; } = string.Empty;

    /// <summary>Extension the catalog is restricted to, without a dot; empty lists everything.</summary>
    public string CatalogFileType { get; set; } = "jpg";

    public CatalogExportType CatalogExportType { get; set; } = CatalogExportType.Csv;
}
