namespace MetricsVisualizer.Services.IO.SharedMemory;

public interface IBackgroundWorker : IAsyncDisposable
{
    Task StartAsync();
    Task StopAsync();
}