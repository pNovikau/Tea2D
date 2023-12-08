using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tea2D.SourceGenerators.Utils;

[InterpolatedStringHandler]
public readonly struct CSharpInterpolatedStringHandler
{
    private readonly StringBuilder _builder;

    public CSharpInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        _builder = new StringBuilder(literalLength);
    }

    public void AppendFormatted<T>(T t)
    {
        switch (t)
        {
            case string str:
                _builder.Append(str);
                break;
            case Func<string> func:
                _builder.Append(func());
                break;
            case IEnumerable enumerable:
                AppendFormattedInternal(enumerable);
                break;
            default:
                _builder.Append(t?.ToString());
                break;
        }
    }

    private void AppendFormattedInternal(IEnumerable enumerable)
    {
        var intendCount = CalculateIntend();

        var enumerator = enumerable.GetEnumerator();
        
        if (enumerator.MoveNext() == false)
            return;

        _builder.AppendLine(enumerator.Current?.ToString());

        while (enumerator.MoveNext())
        {
            AppendIntend(intendCount);
            _builder.AppendLine(enumerator.Current?.ToString());
        }

        _builder.Remove(_builder.Length - 2, 2);
    }

    private void AppendIntend(int intendCount)
    {
        const string intend = "    ";

        for (var i = 0; i < intendCount; i++)
        {
            _builder.Append(intend);
        }
    }

    private int CalculateIntend()
    {
        var spacesCount = 0;
        for (var index = _builder.Length - 1; index >= 0; index--)
        {
            if (_builder[index] != ' ')
                break;

            ++spacesCount;
        }

        return spacesCount / 4;
    }

    public void AppendLiteral(string value)
    {
        _builder.Append(value);
    }

    public override string ToString()
    {
        return _builder.ToString();
    }
}