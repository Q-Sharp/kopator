namespace Kopator.App.Services;

/// <summary>
/// The file and message dialogs a view model needs, kept behind an interface so the
/// view models stay free of window handles.
/// </summary>
public interface IDialogService
{
    /// <summary>Asks for a folder; returns <c>null</c> when the user cancels.</summary>
    Task<string?> PickFolderAsync(string title, string? startAt);

    /// <summary>Asks where to save a file; returns <c>null</c> when the user cancels.</summary>
    Task<string?> PickSaveFileAsync(string title, string suggestedName, string extension, string? startAt);

    Task ShowMessageAsync(string title, string message);
}
