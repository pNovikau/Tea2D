namespace Tea2D.Core.Collections;

public interface IUnorderedList<TItem>
{
    TItem[] Items { get; }

    ref TItem Get();
    void Remove(int id);
    void Clear();
}