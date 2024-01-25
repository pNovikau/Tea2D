using System.Windows;

namespace Tea2D.Trace.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private Thread _thread;

    public MainWindow()
    {
        InitializeComponent();
        _thread = new Thread(Start);
        _thread.Start();

        //_metricListener = new MetricListener();
        //_metricListener.Start();
        //
        //_metricListener.MetricAdded += MetricListenerOnMetricAdded;
        //_metricListener.MetricUpdated += MetricListenerOnMetricUpdated;
    }

    private void Start()
    {
        while (true)
        {
            Thread.Sleep(10);

            Dispatcher.Invoke(Draw);
        }
    }

    public void Draw()
    {
        label1.Value += 1;
        label2.Value += 1;
        label3.Value += 1;
        label4.Value += 1;
    }
}