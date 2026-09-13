using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Kopator.App.Views;

namespace Kopator.App.Services;

/// <summary>
/// Implements the dialogs with Avalonia's storage provider, which maps to the native
/// file chooser on each platform (portal on Linux, common dialogs on Windows).
/// </summary>
public sealed class StorageDialogService(Window owner) : IDialogService
{
    public async Task<string?> PickFolderAsync(string title, string? startAt)
    {
        var folders = await owner.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = title,
            AllowMultiple = false,
            SuggestedStartLocation = await TryGetFolderAsync(startAt),
        });

        return folders.Count > 0 ? folders[0].TryGetLocalPath() : null;
    }

    public async Task<string?> PickSaveFileAsync(string title, string suggestedName, string extension, string? startAt)
    {
        var normalized = extension.Trim().TrimStart('.').ToLowerInvariant();

        var fileType = new FilePickerFileType(normalized.ToUpperInvariant())
        {
            Patterns = [$"*.{normalized}"],
        };

        var file = await owner.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = title,
            SuggestedFileName = suggestedName,
            DefaultExtension = normalized,
            FileTypeChoices = [fileType],
            ShowOverwritePrompt = true,
            SuggestedStartLocation = await TryGetFolderAsync(startAt),
        });

        return file?.TryGetLocalPath();
    }

    public Task ShowMessageAsync(string title, string message) =>
        new MessageWindow(title, message).ShowDialog(owner);

    /// <summary>
    /// Resolves the folder a dialog should open in. A path pointing at a file opens its
    /// parent directory; anything unusable simply lets the platform decide.
    /// </summary>
    private async Task<IStorageFolder?> TryGetFolderAsync(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return null;

        try
        {
            var full = Path.GetFullPath(path);
            var directory = Directory.Exists(full) ? full : Path.GetDirectoryName(full);

            return string.IsNullOrEmpty(directory)
                ? null
                : await owner.StorageProvider.TryGetFolderFromPathAsync(directory);
        }
        catch
        {
            return null;
        }
    }
}
