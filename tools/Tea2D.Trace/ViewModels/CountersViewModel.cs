using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Tea2D.Metrics.Diagnostics;
using Tea2D.Trace.Models.Messages;
using Tea2D.Trace.Services.Messaging;

namespace Tea2D.Trace.ViewModels;

public sealed partial class CountersViewModel : ObservableObject, IMessageHandler<MetricAddedMessage>, IMessageHandler<MetricUpdatedMessage>
{
    [ObservableProperty]
    private ObservableCollection<CounterViewModel> _counters = new();

    [ObservableProperty]
    private Visibility _visibility = Visibility.Visible;

    

    public ValueTask HandleAsync(MetricAddedMessage message)
    {
        if (message.Type == MetricType.Counter)
        {
            Counters.Add(new CounterViewModel { Name = message.Name, Value = 0 });
        }

        return ValueTask.CompletedTask;
    }

    public ValueTask HandleAsync(MetricUpdatedMessage message)
    {
        if (message.Type == MetricType.Counter)
        {
            var counter = Counters.FirstOrDefault(p => p.Name.Equals(message.Name));

            if (counter is not null)
                counter.Value = message.Value;
        }

        return ValueTask.CompletedTask;
    }
}