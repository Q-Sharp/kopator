using Kopator.Core.Settings;

namespace Kopator.Core.Tests;

public class JsonSettingsStoreTests
{
    [Fact]
    public void RoundTripsEveryValue()
    {
        using var directory = new TempDirectory();
        var store = new JsonSettingsStore(directory.Combine("settings.json"));

        var saved = new KopatorSettings
        {
            Mode = KopatorMode.Catalog,
            Move = true,
            CopySource = "/tmp/source",
            CopyDestination = "/tmp/destination",
            CollectPath = "/tmp/collect",
            CollectIgnore = "*.tmp",
            CatalogSource = "/tmp/catalog",
            CatalogDestinationFile = "/tmp/catalog.csv",
            CatalogFileType = "png",
            CatalogExportType = CatalogExportType.Html,
        };

        store.Save(saved);
        var loaded = store.Load();

        Assert.Equivalent(saved, loaded);
    }

    [Fact]
    public void MissingFileYieldsDefaults()
    {
        using var directory = new TempDirectory();

        var loaded = new JsonSettingsStore(directory.Combine("never-written.json")).Load();

        Assert.Equal(KopatorMode.Copy, loaded.Mode);
        Assert.Equal(CatalogExportType.Csv, loaded.CatalogExportType);
        Assert.False(loaded.Move);
    }

    [Fact]
    public void CorruptFileYieldsDefaultsInsteadOfThrowing()
    {
        using var directory = new TempDirectory();
        var path = directory.WriteFile("settings.json", "{ this is not json");

        var loaded = new JsonSettingsStore(path).Load();

        Assert.Equal(KopatorMode.Copy, loaded.Mode);
    }

    [Fact]
    public void SaveCreatesMissingDirectories()
    {
        using var directory = new TempDirectory();
        var path = directory.Combine(Path.Combine("nested", "deeper", "settings.json"));

        new JsonSettingsStore(path).Save(new KopatorSettings { CollectIgnore = "*.bak" });

        Assert.True(File.Exists(path));
        Assert.Equal("*.bak", new JsonSettingsStore(path).Load().CollectIgnore);
    }

    [Fact]
    public void EnumsArePersistedByNameNotOrdinal()
    {
        using var directory = new TempDirectory();
        var path = directory.Combine("settings.json");

        new JsonSettingsStore(path).Save(new KopatorSettings { Mode = KopatorMode.Collect });

        Assert.Contains("\"Collect\"", File.ReadAllText(path), StringComparison.Ordinal);
    }

    [Fact]
    public void DefaultPathLivesUnderApplicationData()
    {
        var path = JsonSettingsStore.DefaultFilePath();

        Assert.EndsWith(Path.Combine("kopator", "settings.json"), path, StringComparison.Ordinal);
        Assert.True(Path.IsPathRooted(path));
    }
}
