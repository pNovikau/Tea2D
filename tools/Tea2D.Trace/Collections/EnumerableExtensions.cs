using System.Collections;

namespace Tea2D.Trace.Collections;

public static class EnumerableExtensions
{
    public static void ForEach<TItem>(this IEnumerable<TItem> enumerable, Action<TItem> action)
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        ArgumentNullException.ThrowIfNull(action);

        foreach (var item in enumerable)
        {
            action(item);
        }
    }

    public static TResult FirstOfType<TResult>(this IEnumerable enumerable)
    {
        ArgumentNullException.ThrowIfNull(enumerable);

        return enumerable.OfType<TResult>().First();
    }
}