namespace MetricsVisualizer.Services.IO.SharedMemory;

public interface ISharedRingBufferListener : IAsyncDisposable
{
    Task StartAsync();
    Task StopAsync();
}