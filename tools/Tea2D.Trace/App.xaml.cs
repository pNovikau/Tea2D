using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Tea2D.Metrics.Diagnostics;
using Tea2D.Trace.Models.Messages;
using Tea2D.Trace.Services.Messaging;
using Tea2D.Trace.Services.Metrics;
using Tea2D.Trace.ViewModels;

namespace Tea2D.Trace;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    private Thread _thread;
    
    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<MetricNamespaceListener>();

        serviceCollection.AddSingleton<IMessagePublisher, MessagePublisher>();

        serviceCollection.AddSingleton<MainWindowViewModel>();
        serviceCollection.AddSingleton<IMessageHandler<MetricAddedMessage>, MainWindowViewModel>();
        serviceCollection.AddSingleton<IMessageHandler<MetricUpdatedMessage>, MainWindowViewModel>();

        Ioc.Default.ConfigureServices(serviceCollection.BuildServiceProvider());

        Ioc.Default.GetRequiredService<IMessagePublisher>().PublishAsync(new MetricAddedMessage("test", MetricType.Counter));
        Ioc.Default.GetRequiredService<IMessagePublisher>().PublishAsync(new MetricAddedMessage("test", MetricType.Histogram));

        _thread = new Thread(Start);
        _thread.Start();
        
        base.OnStartup(e);
    }

    private static void Start()
    {
        Thread.Sleep(100);
        Ioc.Default.GetRequiredService<IMessagePublisher>().PublishAsync(new MetricUpdatedMessage("test", MetricType.Counter, Random.Shared.Next()));
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
    }
}