using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Tea2D.Trace.ViewModels;

namespace Tea2D.Trace.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>();
    }

}