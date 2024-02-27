using CommunityToolkit.Mvvm.ComponentModel;

namespace Tea2D.Trace.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private TabsContainerViewModel _tabsContainer;

    public MainWindowViewModel(TabsContainerViewModel tabsContainerViewModel)
    {
        _tabsContainer = tabsContainerViewModel ?? throw new ArgumentNullException(nameof(tabsContainerViewModel));
    }
}