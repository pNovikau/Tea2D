namespace Tea2D.Trace.Services.Threading;

public abstract class BackgroundWorker : IBackgroundWorker
{
    private readonly CancellationTokenSource _tokenSource = new();

    private Task? _worker;

    private volatile bool _isRunning;

    protected CancellationToken Token => _tokenSource.Token;

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

    protected abstract void Run();

    public async ValueTask DisposeAsync()
    {
        await StopAsync();

        _tokenSource.Dispose();
        _worker?.Dispose();

        GC.SuppressFinalize(this);
    }
}