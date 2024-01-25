namespace Tea2DTrace.Services.IO.SharedMemory;

public interface IBackgroundWorker : IAsyncDisposable
{
    Task StartAsync();
    Task StopAsync();
}