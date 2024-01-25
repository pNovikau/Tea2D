using Tea2D.Trace.Services;

namespace Tea2D.Trace.Views.Controls;

public partial class LongLabel : UserControl
{
    private readonly Graphics _graphics;

    private Font _font = SystemFonts.DefaultFont;
    private SolidBrush _brush = new(Color.Black);
    
    private static readonly SolidBrush DrawBrush = new(Color.Black);

    public LongLabel()
    {
        InitializeComponent();

        _graphics = CreateGraphics();
    }


    public string FontFamily
    {
        get => _font.FontFamily.ToString();
        set => _font = new Font(value, _font.Size);
    }
    
    public float FontSize
    {
        get => _font.Size;
        set => _font = new Font(_font.FontFamily, value);
    }

    public Color Foreground
    {
        get => _brush.Color;
        set => _brush = new SolidBrush(value);
    }

    public long Value
    {
        set
        {
            var digitsCount = value.GetDigitsCount();
            Span<char> text = stackalloc char[digitsCount];
            value.ConvertToSpan(text);

            _graphics.Clear(BackColor);
            _graphics.DrawString(text, _font, _brush, new PointF(0, 0));
        }
    }
}