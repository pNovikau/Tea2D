using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Tea2D.SourceGenerators.Utils;

public static class StringExtensions
{
    [StringFormatMethod(nameof(formatString))]
    public static IEnumerable<string> Format(this string formatString, IEnumerable<string> args, string separator = "")
    {
        var enumerator = args.GetEnumerator();

        if (enumerator.MoveNext() == false)
            yield break;

        bool hasNext;
        string current;

        do
        {
            current = enumerator.Current;
            hasNext = enumerator.MoveNext();

            if (hasNext)
                yield return string.Format(formatString + separator, current);

        } while (hasNext);
        
        yield return string.Format(formatString, current);
        
        enumerator.Dispose();
    }
    
    [StringFormatMethod(nameof(formatString))]
    public static string Format(this string formatString, string val)
    {
        return string.Format(formatString, val);
    }
}