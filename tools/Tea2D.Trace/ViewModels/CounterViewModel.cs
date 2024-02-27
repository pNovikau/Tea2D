using CommunityToolkit.Mvvm.ComponentModel;

namespace Tea2D.Trace.ViewModels;

public sealed partial class CounterViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private long _value;
}