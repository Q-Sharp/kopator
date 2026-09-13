namespace Kopator.Core.Services;

/// <param name="SourcePath">Folder whose files are copied or moved.</param>
/// <param name="DestinationPath">Folder that receives them; created when missing.</param>
/// <param name="Move">Move instead of copy.</param>
public readonly record struct CopyRequest(string SourcePath, string DestinationPath, bool Move);

/// <summary>Copies or moves the files of a single folder - subdirectories are not descended into.</summary>
public sealed class CopyService
{
    public OperationResult Execute(CopyRequest request, IProgress<int>? progress, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.SourcePath) || string.IsNullOrWhiteSpace(request.DestinationPath))
            return OperationResult.Invalid(ValidationError.MissingPath);

        // Moving removes files from the source, so it needs write access there as well.
        if (!DirectoryAccess.Check(request.SourcePath, readable: true, writable: request.Move))
            return OperationResult.Invalid(ValidationError.SourceNotAccessible);

        if (!Directory.Exists(request.DestinationPath))
        {
            try
            {
                Directory.CreateDirectory(request.DestinationPath);
            }
            catch
            {
                return OperationResult.Invalid(ValidationError.DestinationNotCreatable);
            }
        }

        if (!DirectoryAccess.Check(request.DestinationPath, readable: false, writable: true))
            return OperationResult.Invalid(ValidationError.DestinationNotAccessible);

        var files = Directory.GetFiles(request.SourcePath);

        return FileTransfer.Run(files, request.DestinationPath, request.Move, progress, cancellationToken);
    }

    /// <summary>Number of files a run would touch, so the caller can size a progress bar.</summary>
    public static int CountFiles(string sourcePath) =>
        Directory.Exists(sourcePath) ? Directory.GetFiles(sourcePath).Length : 0;
}
