namespace Tea2D.Trace.Services.Threading;

public interface IBackgroundWorker : IAsyncDisposable
{
    Task StartAsync();
    Task StopAsync();
}