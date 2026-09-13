using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kopator.Core.Settings;

/// <summary>
/// Stores settings as JSON under the user's application data directory - on Linux
/// <c>~/.config/kopator/settings.json</c>, on Windows <c>%APPDATA%\kopator\settings.json</c>.
/// </summary>
public sealed class JsonSettingsStore : ISettingsStore
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
    };

    private readonly string _filePath;

    public JsonSettingsStore()
        : this(DefaultFilePath())
    {
    }

    public JsonSettingsStore(string filePath) => _filePath = filePath;

    public static string DefaultFilePath() => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create),
        "kopator",
        "settings.json");

    public KopatorSettings Load()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new KopatorSettings();

            return JsonSerializer.Deserialize<KopatorSettings>(File.ReadAllText(_filePath), Options)
                   ?? new KopatorSettings();
        }
        catch
        {
            // Unreadable or corrupt settings must never keep the application from starting.
            return new KopatorSettings();
        }
    }

    public void Save(KopatorSettings settings)
    {
        try
        {
            var directory = Path.GetDirectoryName(_filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(_filePath, JsonSerializer.Serialize(settings, Options));
        }
        catch
        {
            // Losing the settings of one session is preferable to failing the shutdown.
        }
    }
}
