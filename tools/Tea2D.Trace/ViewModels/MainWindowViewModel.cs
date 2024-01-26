using CommunityToolkit.Mvvm.ComponentModel;

namespace Tea2D.Trace.ViewModels;

public sealed partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private CountersViewModel _countersViewModel;
}