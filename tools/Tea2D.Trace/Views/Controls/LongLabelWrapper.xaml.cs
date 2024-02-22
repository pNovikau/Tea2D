using System.ComponentModel;
using System.Windows;
using System.Windows.Forms.Integration;
using Tea2D.Trace.Views.Utils;

namespace Tea2D.Trace.Views.Controls;

public partial class LongLabelWrapper : UserControl
{
    private static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(long), typeof(LongLabelWrapper), new PropertyMetadata(default(long), ValuePropertyChangedCallback));

    private readonly WindowsFormsHost _windowsFormsHost;
    private readonly LongLabel _longLabel;

    public LongLabelWrapper()
    {
        InitializeComponent();
        InitializePropertyChangeHandler();

        _longLabel = new LongLabel();
        _windowsFormsHost = new WindowsFormsHost();
        _windowsFormsHost.Child = _longLabel;

        Grid.Children.Add(_windowsFormsHost);
    }

    private void InitializePropertyChangeHandler()
    {
        DependencyPropertyDescriptor.FromProperty(ForegroundProperty, typeof(LongLabelWrapper)).AddValueChanged(this, OnForegroundChanged);
        DependencyPropertyDescriptor.FromProperty(FontSizeProperty, typeof(LongLabelWrapper)).AddValueChanged(this, OnFontSizeChanged);
        DependencyPropertyDescriptor.FromProperty(FontFamilyProperty, typeof(LongLabelWrapper)).AddValueChanged(this, OnFontFamilyChanged);
    }

    private static void ValuePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is LongLabelWrapper wrapper)
            wrapper._longLabel.Value = (long)e.NewValue;
    }

    public long Value
    {
        get => (long)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    private void OnForegroundChanged(object? sender, EventArgs e)
    {
        _longLabel.Foreground = WinFormsTypeConverter.Convert(Foreground);
    }

    private void OnFontSizeChanged(object? sender, EventArgs e)
    {
        _longLabel.FontSize = (float)FontSize;
    }
    
    private void OnFontFamilyChanged(object? sender, EventArgs e)
    {
        _longLabel.FontFamily = WinFormsTypeConverter.Convert(FontFamily);
    }
}