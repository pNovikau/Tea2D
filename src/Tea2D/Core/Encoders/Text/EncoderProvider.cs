namespace Tea2D.Core.Encoders.Text;

public static class EncoderProvider
{
    public static readonly IEncoder Utf32 = new Utf32Encoder();
}