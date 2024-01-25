namespace Tea2D.Trace.Services.IO.SharedMemory;

public interface IBackgroundWorker : IAsyncDisposable
{
    Task StartAsync();
    Task StopAsync();
}