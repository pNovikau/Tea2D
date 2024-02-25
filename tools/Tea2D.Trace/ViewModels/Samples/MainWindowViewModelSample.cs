namespace Tea2D.Trace.ViewModels.Samples;

public sealed class MainWindowViewModelSample : MainWindowViewModel
{
    public MainWindowViewModelSample() : base( new TabsContainerViewModelSample())
    {
    }
}