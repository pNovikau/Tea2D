using System;

namespace Tea2D.Core.Collections;

public interface IReusableList<TItem>
{
    ref TItem Get(out int id);
    ref TItem Get(int id);
    void Remove(int id);
    void Clear();
    Span<TItem> AsSpan();
}