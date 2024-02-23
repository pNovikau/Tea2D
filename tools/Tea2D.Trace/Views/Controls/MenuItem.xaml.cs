using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace Tea2D.Trace.Views.Controls;

public partial class MenuItem
{
    public static readonly DependencyProperty FillProperty =
        DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.Transparent));

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(MenuItem), new PropertyMetadata(false, OnIsSelectedChanged));

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(MenuItem), new PropertyMetadata((ImageSource)null!));

    public static readonly DependencyProperty SelectedCommandProperty =
        DependencyProperty.Register(nameof(SelectedCommand), typeof(ICommand), typeof(MenuItem), new PropertyMetadata((ICommand)null!));

    public static readonly DependencyProperty UnselectedCommandProperty =
        DependencyProperty.Register(nameof(UnselectedCommand), typeof(ICommand), typeof(MenuItem), new PropertyMetadata((ICommand)null!));

    public MenuItem()
    {
        InitializeComponent();
    }

    public Brush Fill
    {
        get => (Brush)GetValue(FillProperty);
        set => SetValue(FillProperty, value);
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

    public ICommand? UnselectedCommand
    {
        get => (ICommand)GetValue(UnselectedCommandProperty);
        set => SetValue(UnselectedCommandProperty, value);
    }

    private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not MenuItem menuItem)
            return;

        if (menuItem.IsSelected)
        {
            if (menuItem.SelectedCommand?.CanExecute(null) is true)
                menuItem.SelectedCommand.Execute(null);
        }
        else
        {
            if (menuItem.UnselectedCommand?.CanExecute(null) is true)
                menuItem.UnselectedCommand.Execute(null);
        }
    }
}