using System.Windows.Media;
using WinFormsBrush = System.Windows.Media.Brush;
using WinFormsFontFamily = System.Windows.Media.FontFamily;

namespace Tea2D.Trace.Views.Utils;

public static class WinFormsTypeConverter
{
    public static Color Convert(WinFormsBrush brush)
    {
        ArgumentNullException.ThrowIfNull(brush);

        if (brush is not SolidColorBrush solidColorBrush)
            throw new InvalidOperationException();

        return Color.FromArgb(solidColorBrush.Color.A, solidColorBrush.Color.R, solidColorBrush.Color.G, solidColorBrush.Color.B);
    }

    public static string Convert(WinFormsFontFamily fontFamily)
    {
        ArgumentNullException.ThrowIfNull(fontFamily);

        return fontFamily.Source;
    }
}