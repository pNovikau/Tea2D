namespace Tea2D.Trace.Services;

public static class LongConverter
{
    public static int GetDigitsCount(this long val) => (int)Math.Floor(Math.Log10(val)) + 1;

    public static void ConvertToSpan(this long value, Span<char> span)
    {
        var val = value;

        for (var i = span.Length - 1; i >= 0; i--)
        {
            span[i] = (char)(val % 10 + '0');
            val /= 10;
        }
    }
}