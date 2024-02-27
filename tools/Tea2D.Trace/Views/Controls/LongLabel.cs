using Tea2D.Trace.Services;

namespace Tea2D.Trace.Views.Controls;

public partial class LongLabel : System.Windows.Forms.UserControl
{
    private readonly Graphics _graphics;

    private long _value;

    private Font _font = SystemFonts.DefaultFont;
    private SolidBrush _brush = new(Color.Black);

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
        get => _value;
        set
        {
            _value = value;

            var digitsCount = value.GetDigitsCount();
            Span<char> text = stackalloc char[digitsCount];
            value.ConvertToSpan(text);

            var stringSize = _graphics.MeasureString(text, _font);
            var xPosition = 0;
            var yPosition = (Height - stringSize.Height) / 2;

            _graphics.Clear(BackColor);
            _graphics.DrawString(text, _font, _brush, new PointF(xPosition, yPosition));
        }
    }
}