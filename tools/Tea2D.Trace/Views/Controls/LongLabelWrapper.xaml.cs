using System.Windows;
using System.Windows.Forms.Integration;
using UserControl = System.Windows.Controls.UserControl;

namespace Tea2D.Trace.Views.Controls;

public partial class LongLabelWrapper : UserControl
{
    private readonly WindowsFormsHost _windowsFormsHost;
    private readonly LongLabel _longLabel;

    public LongLabelWrapper()
    {
        InitializeComponent();

        _longLabel = new LongLabel();
        _windowsFormsHost = new WindowsFormsHost();
        _windowsFormsHost.Child = _longLabel;

        Grid.Children.Add(_windowsFormsHost);

        //FontSizeProperty.OverrideMetadata(typeof(LongLabelWrapper), new PropertyMetadata(System.Windows.SystemFonts.MessageFontSize, FontSizeChangedCallback));
        //FontFamilyProperty.OverrideMetadata(typeof(LongLabelWrapper), new PropertyMetadata(System.Windows.SystemFonts.MessageFontFamily, FontFamilyChangedCallback));
    }

    private long _value;
    public long Value
    {
        get => _value;
        set
        {
            _value = value;
            _longLabel.Value = value;
        }
    }

    private void FontSizeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        _longLabel.FontSize = (float)e.NewValue;
    }
    
    private void FontFamilyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        _longLabel.FontFamily = ((System.Windows.Media.FontFamily)e.NewValue).Source;
    }
}