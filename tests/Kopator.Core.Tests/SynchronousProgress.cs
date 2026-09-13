namespace Kopator.Core.Tests;

/// <summary>
/// Reports on the calling thread. <see cref="Progress{T}"/> posts to a synchronization
/// context, which would make assertions in a test race against the callback.
/// </summary>
public sealed class SynchronousProgress<T>(Action<T> handler) : IProgress<T>
{
    public void Report(T value) => handler(value);
}
