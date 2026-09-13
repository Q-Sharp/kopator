namespace Kopator.Core.Settings;

/// <summary>Loads and saves <see cref="KopatorSettings"/>.</summary>
public interface ISettingsStore
{
    /// <summary>Returns the stored settings, or defaults when nothing usable is stored.</summary>
    KopatorSettings Load();

    void Save(KopatorSettings settings);
}
