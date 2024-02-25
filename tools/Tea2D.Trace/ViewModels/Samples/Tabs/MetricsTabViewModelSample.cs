using System.Windows;
using Tea2D.Trace.ViewModels.Tabs;

namespace Tea2D.Trace.ViewModels.Samples.Tabs;

public sealed class MetricsTabViewModelSample : MetricsTabViewModel
{
    public MetricsTabViewModelSample()
    {
        ContentVisibility = Visibility.Visible;
        Counters =
        [
            new CounterViewModel { Name = "counter-1", Value = 12 },
            new CounterViewModel { Name = "counter-2", Value = 13 },
            new CounterViewModel { Name = "counter-3", Value = 14 },
            new CounterViewModel { Name = "counter-4", Value = 15 }
        ];
    }
}