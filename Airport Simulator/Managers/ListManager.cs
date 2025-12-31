using Airport_Simulator.Interfaces;

namespace Airport_Simulator.Managers;

public class ListManager<T> : IListManager<T>
{
    private List<T> m_List = [];
    public int Count => m_List.Count;
    public void Remove(T item) => m_List.Remove(item);
    public bool Contains(T item) => m_List.Contains(item);
    public bool ContainsIndex(int index) => index >= 0 && index < m_List.Count;
    public T Get(int index) => m_List[index];
    public void Clear() => m_List.Clear();
    public string[] ToStringArray() => ToStringList().ToArray();
    public List<string> ToStringList() => m_List.Select(item => item.ToString()).ToList();
    public void Sort(IComparer<T> comparer) => m_List.Sort(comparer);

    public bool Add(T item)
    {
        if (item == null) return false;

        m_List.Add(item);
        return true;
    }

    public bool RemoveAt(int index)
    {
        if (!ContainsIndex(index)) return false;

        m_List.RemoveAt(index);
        return true;
    }

    public bool Replace(T item, int index)
    {
        if (!ContainsIndex(index)) return false;
        m_List[index] = item;
        return true;
    }
}