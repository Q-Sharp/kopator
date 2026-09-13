namespace Kopator.Core;

/// <summary>How an operation ended.</summary>
public enum OperationOutcome
{
    /// <summary>Ran to completion.</summary>
    Completed,

    /// <summary>Stopped early because the caller cancelled.</summary>
    Cancelled,

    /// <summary>Rejected before doing any work; <see cref="OperationResult.Error"/> says why.</summary>
    Invalid,
}

/// <summary>
/// Outcome of a single operation. Services never talk to the user directly - they
/// report what happened and let the caller decide how to present it.
/// </summary>
/// <param name="Outcome">How the operation ended.</param>
/// <param name="ProcessedCount">Number of items successfully handled.</param>
/// <param name="FailedCount">Number of items that threw and were skipped.</param>
/// <param name="Error">Reason for an <see cref="OperationOutcome.Invalid"/> result.</param>
public readonly record struct OperationResult(
    OperationOutcome Outcome,
    int ProcessedCount = 0,
    int FailedCount = 0,
    ValidationError? Error = null)
{
    public static OperationResult Completed(int processed, int failed = 0) =>
        new(OperationOutcome.Completed, processed, failed);

    public static OperationResult Cancelled(int processed, int failed = 0) =>
        new(OperationOutcome.Cancelled, processed, failed);

    public static OperationResult Invalid(ValidationError error) =>
        new(OperationOutcome.Invalid, Error: error);
}

/// <summary>Why an operation was rejected before it started.</summary>
public enum ValidationError
{
    /// <summary>A required path was left empty.</summary>
    MissingPath,

    /// <summary>The source folder cannot be read, or cannot be written when moving.</summary>
    SourceNotAccessible,

    /// <summary>The destination folder cannot be written to.</summary>
    DestinationNotAccessible,

    /// <summary>The destination folder does not exist and could not be created.</summary>
    DestinationNotCreatable,
}
