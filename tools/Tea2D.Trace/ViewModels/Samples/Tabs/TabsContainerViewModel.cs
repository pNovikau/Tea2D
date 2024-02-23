using CommunityToolkit.Mvvm.ComponentModel;
using Tea2D.Trace.ViewModels.Tabs;

namespace Tea2D.Trace.ViewModels.Samples.Tabs;

public partial class TabsContainerViewModel : ObservableObject
{
    private readonly MetricsTabViewModel _metricsTabViewModel;
    
    public TabsContainerViewModel(MetricsTabViewModel metricsTabViewModel)
    {
        _metricsTabViewModel = metricsTabViewModel ?? throw new ArgumentNullException(nameof(metricsTabViewModel));
    }
}