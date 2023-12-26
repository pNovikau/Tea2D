namespace MetricsVisualizer;

public static class MetricListener
{
    private static CancellationTokenSource _cancellationTokenSource;
    private static Task _workerTask;

    public static void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _workerTask = Task.Factory.StartNew(DoWork, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    private static void DoWork()
    {
        
    }

    public static void Stop()
    {
        _cancellationTokenSource?.Cancel();
        _workerTask?.Dispose();
    }
}