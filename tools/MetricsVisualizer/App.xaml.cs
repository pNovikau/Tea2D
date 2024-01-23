using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using MetricsVisualizer.Services.Metrics;
using Microsoft.Extensions.DependencyInjection;

namespace MetricsVisualizer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<MetricNamespaceListener>();

        Ioc.Default.ConfigureServices(serviceCollection.BuildServiceProvider());
        
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
    }
}