using MetricsVisualizer.Services.Metrics;
using Tea2D.Metrics.IO.SharedMemory;

namespace MetricsVisualizer.Services.IO.SharedMemory;

public abstract class SharedRingBufferListener<TItem> : ISharedRingBufferListener
    where TItem : struct
{
    private readonly string _name;
    private readonly CancellationTokenSource _tokenSource = new();

    private Task? _worker;

    private volatile bool _isRunning;

    protected SharedRingBufferListener(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        _name = name;
    }

    public async Task StartAsync()
    {
        await StopAsync();

        _worker = Task.Factory.StartNew(Run, _tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

        _isRunning = true;
    }

    public async Task StopAsync()
    {
        if (!_isRunning)
            return;

        await _tokenSource.CancelAsync();
        await _worker!;

        _isRunning = false;
    }

    private void Run()
    {
        var cancellationToken = _tokenSource.Token;

        MemoryMappedFileHelper.WaitForMemoryMappedFile("", cancellationToken);

        using var pipeReader = new PipeReader<TItem>(_name);

        while (cancellationToken.IsCancellationRequested is false && pipeReader.Read(out var item))
            HandleItem(item);
    }

    protected abstract void HandleItem(TItem item);

    public async ValueTask DisposeAsync()
    {
        await StopAsync();

        _tokenSource.Dispose();
        _worker?.Dispose();

        GC.SuppressFinalize(this);
    }
}