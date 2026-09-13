using System.Globalization;
using Avalonia.Data.Converters;
using Kopator.Core;

namespace Kopator.App.Converters;

/// <summary>
/// Displays <see cref="CatalogExportType"/> as the upper-case acronym the original
/// application showed ("CSV", "HTML") rather than the PascalCase enum member name.
/// </summary>
public sealed class ExportTypeNameConverter : IValueConverter
{
    public static ExportTypeNameConverter Instance { get; } = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is CatalogExportType exportType ? exportType.ToString().ToUpperInvariant() : value?.ToString();

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is string text && Enum.TryParse<CatalogExportType>(text, ignoreCase: true, out var parsed)
            ? parsed
            : null;
}
