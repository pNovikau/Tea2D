namespace Tea2D.Common.Collections;

public interface IUnorderedList<TItem>
{
    TItem[] Items { get; }

    ref TItem Get();
    void Remove(int id);
    void Clear();
}