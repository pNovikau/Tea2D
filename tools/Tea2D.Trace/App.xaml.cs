using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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
    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<MetricNamespaceListener>();

        serviceCollection.AddSingleton<MainWindowViewModel>();
        serviceCollection.AddSingleton<CountersViewModel>();

        serviceCollection.AddMessaging();

        serviceCollection.AddSingleton<IMessageHandler<MetricAddedMessage>>(p => new DispatcherMessageHandlerDecorator<MetricAddedMessage>(p.GetRequiredService<CountersViewModel>()));
        serviceCollection.AddSingleton<IMessageHandler<MetricUpdatedMessage>>(p => new DispatcherMessageHandlerDecorator<MetricUpdatedMessage>(p.GetRequiredService<CountersViewModel>()));

        Ioc.Default.ConfigureServices(serviceCollection.BuildServiceProvider());

        Ioc.Default.GetRequiredService<MetricNamespaceListener>().StartAsync().GetAwaiter().GetResult();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Ioc.Default.GetRequiredService<MetricNamespaceListener>().StopAsync().GetAwaiter().GetResult();

        base.OnExit(e);
    }
}