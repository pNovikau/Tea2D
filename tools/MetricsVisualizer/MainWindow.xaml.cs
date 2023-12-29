using System.Windows;
using System.Windows.Controls;
using Tea2D.Metrics.Diagnostics;

namespace MetricsVisualizer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Dictionary<string, (MetricType type, long val, TextBlock text)> _metrics = new();
    private readonly MetricListener _metricListener;

    public MainWindow()
    {
        InitializeComponent();

        _metricListener = new MetricListener();
        _metricListener.Start();

        _metricListener.MetricAdded += MetricListenerOnMetricAdded;
        _metricListener.MetricUpdated += MetricListenerOnMetricUpdated;
    }

    private void MetricListenerOnMetricUpdated(object? sender, MeasureEventArgs e)
    {
        Dispatcher.Invoke(() =>
        {
            var (type, val, text) = _metrics[e.Name];

            if (type == MetricType.Histogram)
                text.Text = $"[{e.Name}] {e.Value}";
            else
            {
                val += e.Value;
                text.Text = $"[{e.Name}] {val}";
            }

            _metrics[e.Name] = (type, val, text);
        });
    }

    private void MetricListenerOnMetricAdded(object? sender, MetricRegisteredEventArgs e)
    {
        Dispatcher.Invoke(() =>
        {
            var textBlock = new TextBlock();
            Panel.Children.Add(textBlock);

            _metrics[e.Name] = (e.Type, 0, textBlock);
        });
    }
}