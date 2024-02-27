using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace Tea2D.Trace.Views.Controls;

public partial class TabItem
{
    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(TabItem), new PropertyMetadata(false, OnIsSelectedChanged));

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(TabItem), new PropertyMetadata(null, OnSourceChanged));

    public static readonly DependencyProperty SelectedCommandProperty =
        DependencyProperty.Register(nameof(SelectedCommand), typeof(ICommand), typeof(TabItem), new PropertyMetadata((ICommand)null!));

    public static readonly DependencyProperty SelectedCommandParameterProperty =
        DependencyProperty.Register(nameof(SelectedCommandParameter), typeof(object), typeof(TabItem), new PropertyMetadata((object)null!));

    public static readonly DependencyProperty UnselectedCommandProperty =
        DependencyProperty.Register(nameof(UnselectedCommand), typeof(ICommand), typeof(TabItem), new PropertyMetadata((ICommand)null!));

    public static readonly DependencyProperty UnselectedCommandParameterProperty =
        DependencyProperty.Register(nameof(UnselectedCommandParameter), typeof(object), typeof(TabItem), new PropertyMetadata((object)null!));

    public TabItem()
    {
        InitializeComponent();
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public ImageSource Source
    {
        get => (ImageSource)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public ICommand? SelectedCommand
    {
        get => (ICommand)GetValue(SelectedCommandProperty);
        set => SetValue(SelectedCommandProperty, value);
    }

    public object? SelectedCommandParameter
    {
        get => GetValue(SelectedCommandParameterProperty);
        set => SetValue(SelectedCommandParameterProperty, value);
    }

    public ICommand? UnselectedCommand
    {
        get => (ICommand)GetValue(UnselectedCommandProperty);
        set => SetValue(UnselectedCommandProperty, value);
    }

    public object? UnselectedCommandParameter
    {
        get => GetValue(UnselectedCommandParameterProperty);
        set => SetValue(UnselectedCommandParameterProperty, value);
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        IsSelected = !IsSelected;

        base.OnMouseLeftButtonUp(e);
    }

    private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TabItem menuItem)
            return;

        if (menuItem.IsSelected)
        {
            if (menuItem.SelectedCommand?.CanExecute(menuItem.SelectedCommandParameter) is true)
                menuItem.SelectedCommand.Execute(menuItem.SelectedCommandParameter);
        }
        else
        {
            if (menuItem.UnselectedCommand?.CanExecute(menuItem.UnselectedCommandParameter) is true)
                menuItem.UnselectedCommand.Execute(menuItem.UnselectedCommandParameter);
        }
    }

    private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TabItem menuItem)
            return;

        menuItem.Image.Source = (ImageSource)e.NewValue;
    }
}