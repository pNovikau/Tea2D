using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Tea2D.Metrics.Diagnostics;
using Tea2D.Trace.Models.Messages;
using Tea2D.Trace.Services.Messaging;

namespace Tea2D.Trace.ViewModels;

public sealed partial class MainWindowViewModel : ObservableObject, IMessageHandler<MetricAddedMessage>, IMessageHandler<MetricUpdatedMessage>
{
    [ObservableProperty] private readonly ObservableCollection<CounterViewModel> _counters = [];

    public ValueTask HandleAsync(MetricAddedMessage message)
    {
        if (message.Type == MetricType.Counter)
            Counters.Add(new CounterViewModel { Name = message.Name, Value = 0 });

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