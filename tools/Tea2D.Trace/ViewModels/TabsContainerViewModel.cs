using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tea2D.Trace.ViewModels.Tabs;

namespace Tea2D.Trace.ViewModels;

public partial class TabsContainerViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<TabBaseViewModel> _tabs;
    [ObservableProperty] private TabBaseViewModel? _selectedTab;

    public TabsContainerViewModel(IEnumerable<TabBaseViewModel> tabs)
    {
        ArgumentNullException.ThrowIfNull(tabs);

        Tabs = new ObservableCollection<TabBaseViewModel>(tabs);
    }

    [RelayCommand]
    private void SelectTab(object param)
    {
        if (param is not TabBaseViewModel newSelectedTab)
            return;

        if (SelectedTab is not null) 
            SelectedTab.IsSelected = false;

        newSelectedTab.IsSelected = true;

        SelectedTab = newSelectedTab;
    }

    [RelayCommand]
    private void UnselectTab(object param)
    {
        if (param is not TabBaseViewModel)
            return;

        if (SelectedTab is null)
            return;

        SelectedTab.IsSelected = false;

        SelectedTab = null;
    }
}