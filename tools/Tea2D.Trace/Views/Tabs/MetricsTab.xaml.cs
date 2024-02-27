using CommunityToolkit.Mvvm.DependencyInjection;
using Tea2D.Trace.ViewModels.Tabs;

namespace Tea2D.Trace.Views.Tabs;

public partial class MetricsTab
{
    public MetricsTab()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<MetricsTabViewModel>();
    }
}