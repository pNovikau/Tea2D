using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Tea2D.Trace.ViewModels.Tabs;

public abstract partial class TabBaseViewModel : ObservableObject
{
    [ObservableProperty] private bool _isSelected;
    [ObservableProperty] private ImageSource? _source;
    [ObservableProperty] private Visibility _contentVisibility = Visibility.Collapsed;

    partial void OnIsSelectedChanged(bool value)
    {
        ContentVisibility = value
            ? Visibility.Visible
            : Visibility.Collapsed;
    }
}