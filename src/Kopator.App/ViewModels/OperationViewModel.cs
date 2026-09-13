using Kopator.App.Services;
using Kopator.Core;
using Kopator.Core.Settings;

namespace Kopator.App.ViewModels;

/// <summary>
/// Base class for the three tabs. A tab knows how to validate and run its operation and
/// how to describe the outcome; the window around it owns the progress bar and buttons.
/// </summary>
public abstract partial class OperationViewModel(IDialogService dialogs) : ViewModelBase
{
    protected IDialogService Dialogs { get; } = dialogs;

    /// <summary>The mode this tab represents; also its position in the tab strip.</summary>
    public abstract KopatorMode Mode { get; }

    /// <summary>Tab header.</summary>
    public abstract string Header { get; }

    /// <summary>Label of the action button while idle.</summary>
    public abstract string ActionLabel { get; }

    /// <summary>Number of items the next run will process, used to size the progress bar.</summary>
    public abstract int CountItems();

    /// <summary>Runs the operation on a background thread.</summary>
    public abstract Task<OperationResult> ExecuteAsync(IProgress<int> progress, CancellationToken cancellationToken);

    /// <summary>Message shown when a run finishes, e.g. "Kopiervorgang abgeschlossen!".</summary>
    public abstract string CompletedMessage { get; }

    /// <summary>Message shown when the user stops a run.</summary>
    public abstract string CancelledMessage { get; }

    public abstract void LoadFrom(KopatorSettings settings);

    public abstract void SaveTo(KopatorSettings settings);

    /// <summary>Turns a rejected run into the German text the user sees.</summary>
    public virtual string DescribeError(ValidationError error) => error switch
    {
        ValidationError.MissingPath => "Bitte Ziel- und Quellordner angeben!",
        ValidationError.SourceNotAccessible => "Keine Lese- und/oder Schreibrechte für den Quellordner!",
        ValidationError.DestinationNotAccessible => "Keine Schreibrechte für den Zielordner!",
        ValidationError.DestinationNotCreatable => "Der Zielordner konnte nicht angelegt werden!",
        _ => "Der Vorgang konnte nicht gestartet werden!",
    };

    /// <summary>
    /// Shows a folder picker and writes the result back, leaving the current value
    /// untouched when the user cancels.
    /// </summary>
    protected async Task<string?> PickFolderAsync(string title, string? current)
    {
        var picked = await Dialogs.PickFolderAsync(title, current);
        return picked ?? current;
    }
}
