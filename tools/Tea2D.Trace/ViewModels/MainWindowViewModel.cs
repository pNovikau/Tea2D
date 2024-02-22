using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tea2D.Trace.ViewModels.Tabs;

namespace Tea2D.Trace.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private MetricsTabViewModel _metricsTab;

    public MainWindowViewModel(MetricsTabViewModel metricsTab)
    {
        _metricsTab = metricsTab ?? throw new ArgumentNullException(nameof(metricsTab));
    }

    [RelayCommand]
    private void ViewOrHideMetricsTab()
    {
        MetricsTab.Visibility = MetricsTab.Visibility == Visibility.Visible
            ? Visibility.Collapsed
            : Visibility.Visible;
    }
}