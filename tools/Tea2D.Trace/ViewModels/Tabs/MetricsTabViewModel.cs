using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Tea2D.Metrics.Diagnostics;
using Tea2D.Trace.Models.Messages;
using Tea2D.Trace.Services.Messaging;

namespace Tea2D.Trace.ViewModels.Tabs;

public partial class MetricsTabViewModel : ObservableObject, IMessageHandler<MetricAddedMessage>, IMessageHandler<MetricUpdatedMessage>
{
    [ObservableProperty]
    private ObservableCollection<CounterViewModel> _counters = [];

    [ObservableProperty]
    private Visibility _visibility = Visibility.Visible;

    public ValueTask HandleAsync(MetricAddedMessage message)
    {
        return message.Type switch
        {
            MetricType.Counter => HandleCounterAddedMessageAsync(message),
            _ => ValueTask.CompletedTask
        };
    }

    private ValueTask HandleCounterAddedMessageAsync(MetricAddedMessage message)
    {
        Counters.Add(new CounterViewModel { Name = message.Name, Value = 0 });

        return ValueTask.CompletedTask;
    }

    public ValueTask HandleAsync(MetricUpdatedMessage message)
    {
        return message.Type switch
        {
            MetricType.Counter => HandleCounterUpdatedMessageAsync(message),
            _ => ValueTask.CompletedTask
        };
    }

    private ValueTask HandleCounterUpdatedMessageAsync(MetricUpdatedMessage message)
    {
        var counter = Counters.FirstOrDefault(p => p.Name.Equals(message.Name));

        if (counter is not null)
            counter.Value = message.Value;

        return ValueTask.CompletedTask;
    }
}