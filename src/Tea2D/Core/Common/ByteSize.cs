using System;

namespace Tea2D.Core.Common;

public readonly struct ByteSize
{
    public ByteSize(long bits)
    {
        Bits = bits;
        Bytes = bits / 8;
        Kilobyte = Bytes / 1024.0;
        Megabyte = Kilobyte / 1024.0;
    }

    public long Bits { get; }
    public long Bytes { get; }
    public double Kilobyte { get; }
    public double Megabyte { get; }

    public static bool operator ==(ByteSize b1, ByteSize b2)
        => b1.Bits == b2.Bits;

    public static bool operator !=(ByteSize b1, ByteSize b2)
        => b1.Bits != b2.Bits;

    public static bool operator >(ByteSize b1, ByteSize b2)
        => b1.Bits > b2.Bits;

    public static bool operator >=(ByteSize b1, ByteSize b2)
        => b1.Bits >= b2.Bits;

    public static bool operator <(ByteSize b1, ByteSize b2)
        => b1.Bits < b2.Bits;

    public static bool operator <=(ByteSize b1, ByteSize b2)
        => b1.Bits <= b2.Bits;

    public bool Equals(ByteSize other)
        => Bits == other.Bits;

    public override bool Equals(object obj)
        => obj is ByteSize other && Equals(other);

    public override int GetHashCode()
        => HashCode.Combine(Bits);

    public static ByteSize FromBytes(double bytes) 
        => new((long)bytes * 8);
    
    public static ByteSize FromKilobytes(double kilobytes) 
        => new((long)kilobytes * 1024 * 8);

    public static ByteSize FromMegabytes(double megabytes) 
        => new((long)(megabytes * 1024 * 1024 * 8));
}