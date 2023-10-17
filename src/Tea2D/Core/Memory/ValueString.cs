using System;
using System.Diagnostics;
using CommunityToolkit.HighPerformance;

namespace Tea2D.Core.Memory;

public static class ValueStringExtensions
{
    public static ValueString AsValueString(this string @string, Span<char> buffer) => new(@string, buffer);
}

[DebuggerDisplay("{ToString(),raw}")]
public ref struct ValueString
{
    private readonly Span<char> _buffer;

    private Span<char> _span;

    public ValueString(Span<char> buffer)
    {
        _buffer = buffer;
        _span = Span<char>.Empty;
    }

    public ValueString(ReadOnlySpan<char> @string, Span<char> buffer)
    {
        Debug.Assert(buffer.Length >= @string.Length);

        @string.CopyTo(buffer);

        _buffer = buffer;
        _span = buffer[..@string.Length];
    }

    public int Length => _span.Length;
    public bool IsEmpty => _span.IsEmpty;

    public void Append(char symbol)
    {
        Debug.Assert(_buffer.Length >= _span.Length + 1);

        _buffer[_span.Length] = symbol;
        _span = _buffer[..(_span.Length + 1)];
    }

    public void Append(int value)
    {
        value.TryFormat(_buffer[_span.Length..], out var charsWritten);

        Debug.Assert(_buffer.Length >= _span.Length + charsWritten);
        _span = _buffer[..(_span.Length + charsWritten)];
    }

    public void Append(ReadOnlySpan<char> @string)
    {
        Debug.Assert(_buffer.Length >= @string.Length + _span.Length);

        @string.CopyTo(_buffer[_span.Length..]);
        _span = _buffer[..(@string.Length + _span.Length)];
    }

    public void Clear()
    {
        if (!IsEmpty)
            _span = _buffer[..0];
    }

    public void Replace(ReadOnlySpan<char> toReplace, ReadOnlySpan<char> replaceWith)
    {
        Debug.Assert(_buffer.Length >= _span.Length + replaceWith.Length - toReplace.Length);
        Debug.Assert(!toReplace.SequenceEqual(replaceWith));

        var foundIndex = _span.IndexOf(toReplace);
        var currentIndex = 0;

        if (foundIndex == -1)
            return;

        Span<char> buffer = stackalloc char[_buffer.Length];
        var bufferIndex = 0;

        while (foundIndex != -1)
        {
            if (currentIndex != foundIndex)
            {
                _span[currentIndex..foundIndex].CopyTo(buffer[bufferIndex..]);
                bufferIndex += foundIndex - currentIndex;
            }

            replaceWith.CopyTo(buffer[bufferIndex..]);
            bufferIndex += replaceWith.Length;

            foundIndex += toReplace.Length;
            currentIndex = foundIndex;

            if (foundIndex >= _span.Length)
                break;

            var newFoundIndex = _span[foundIndex..].IndexOf(toReplace);
            foundIndex = newFoundIndex == -1 ? -1 : newFoundIndex + foundIndex;
        }

        _span[currentIndex..].CopyTo(buffer[bufferIndex..]);
        bufferIndex += _span.Length - currentIndex;

        buffer.CopyTo(_buffer);
        _span = _buffer[..bufferIndex];
    }

    public void Remove(int startIndex)
    {
        Debug.Assert(startIndex < _span.Length);
        Debug.Assert(startIndex > 0);

        _span = _buffer[..startIndex];
    }

    public void Trim()
    {
        TrimStart();
        TrimEnd();
    }

    public void Trim(char symbol)
    {
        TrimStart(symbol);
        TrimEnd(symbol);
    }

    public void Trim(params char[] symbols)
    {
        TrimStart(symbols);
        TrimEnd(symbols);
    }

    public void TrimEnd() => TrimEnd(' ');

    public void TrimEnd(char symbol)
    {
        if (IsEmpty)
            return;

        var index = _span.Length - 1;

        for (var i = index; i >= 0; i--)
        {
            if (index != i)
            {
                if (index != _span.Length - 1)
                    _span = _span[..(index + 1)];

                return;
            }

            if (_span[i] == symbol)
                --index;
        }
    }

    public void TrimEnd(params char[] symbols)
    {
        if (IsEmpty)
            return;

        var index = _span.Length - 1;

        for (var i = index; i >= 0; i--)
        {
            if (index != i)
            {
                if (index != _span.Length - 1)
                    _span = _span[..(index + 1)];

                return;
            }

            var symbol = _span[i];

            for (var j = 0; j < symbols.Length; j++)
            {
                var compareTo = symbols[j];

                if (symbol == compareTo)
                {
                    --index;
                    break;
                }
            }
        }
    }


    public void TrimStart() => TrimStart(' ');

    public void TrimStart(char symbol)
    {
        if (IsEmpty)
            return;

        var index = 0;

        for (var i = 0; i < _span.Length; i++)
        {
            if (index != i)
                break;

            if (symbol == _span[i])
                ++index;
        }

        ShiftLeft(index);
    }

    public void TrimStart(params char[] symbols)
    {
        if (IsEmpty)
            return;

        var index = 0;

        for (var i = 0; i < _span.Length; i++)
        {
            if (index != i)
                break;

            var symbol = _span[i];

            for (var j = 0; j < symbols.Length; j++)
            {
                var compareTo = symbols[j];

                if (symbol == compareTo)
                {
                    ++index;
                    break;
                }
            }
        }

        ShiftLeft(index);
    }

    public Span<char> AsSpan() => _span;

    private void ShiftLeft(int count)
    {
        if (count == 0)
            return;

        if (count == _span.Length)
        {
            Clear();

            return;
        }

        Span<char> buffer = stackalloc char[_buffer.Length];
        _buffer[count..].CopyTo(buffer);
        buffer.CopyTo(_buffer);

        _span = _buffer[..(_span.Length - count)];
    }

    public static implicit operator ReadOnlySpan<char>(ValueString valueString) => valueString._span;

    public override int GetHashCode() => _span.GetDjb2HashCode();

    public override string ToString() => IsEmpty ? string.Empty : new string(_span);
}