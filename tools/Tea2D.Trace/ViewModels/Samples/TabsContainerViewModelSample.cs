using Tea2D.Trace.ViewModels.Samples.Tabs;
using Tea2D.Trace.ViewModels.Tabs;

namespace Tea2D.Trace.ViewModels.Samples;

public sealed class TabsContainerViewModelSample : TabsContainerViewModel
{
    public TabsContainerViewModelSample() : base(new TabBaseViewModel[]
    {
        new MetricsTabViewModelSample { IsSelected = true },
        new MetricsTabViewModelSample(),
        new MetricsTabViewModelSample(),
        new MetricsTabViewModelSample(),
        new MetricsTabViewModelSample(),
    })
    {
    }
}