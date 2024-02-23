using System.Collections;
using System.Windows;
using System.Windows.Markup;

namespace Tea2D.Trace.Views.Controls;

[ContentProperty(nameof(MainContent))]
public sealed partial class Menu
{
    public static readonly DependencyProperty ItemsSourceProperty = 
        DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(Menu), new PropertyMetadata((IEnumerable)null!));

    public static readonly DependencyProperty MainContentProperty =
        DependencyProperty.Register(nameof(MainContent), typeof(object), typeof(Menu), new PropertyMetadata(null, OnMainContentChanged));

    public Menu()
    {
        InitializeComponent();
    }

    public IEnumerable? ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    
    public object MainContent
    {
        get => GetValue(MainContentProperty);
        set => SetValue(MainContentProperty, value);
    }

    private static void OnMainContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Menu { ItemsSource: not null } menu)
            return;

        foreach (var item in menu.ItemsSource)
        {
            
        }
    }
}