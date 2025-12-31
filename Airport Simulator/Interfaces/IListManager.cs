namespace Airport_Simulator.Interfaces;

public interface IListManager<T>
{
    /// <summary>
    /// List count
    /// </summary>
    public int Count { get; }
    
    /// <summary>
    /// Add item to List
    /// </summary>
    /// <param name="item">of type T</param>
    /// <returns>True if item T was added</returns>
    public bool Add(T item);
    
    /// <summary>
    /// Remove item
    /// </summary>
    /// <param name="item">Item T to be removed</param>
    public void Remove(T item);
    
    /// <summary>
    /// Remove T at index
    /// </summary>
    /// <param name="index">The index to remove item at</param>
    /// <returns>True if item was removed, false if index was out of range</returns>
    public bool RemoveAt(int index);
    
    /// <summary>
    /// Replace item at index
    /// </summary>
    /// <param name="item">Item to replace with</param>
    /// <param name="index">Index to replace at</param>
    /// <returns>True if item was replaced at index, false if index was out of range</returns>
    public bool Replace(T item, int index);
    
    /// <summary>
    /// Does List contain item T
    /// </summary>
    /// <param name="item">T item to check if it exists in list</param>
    /// <returns>True if item T was found in list</returns>
    public bool Contains(T item);

    /// <summary>
    /// Check if index exists in list
    /// </summary>
    /// <param name="index">index to look up</param>
    /// <returns>True if index exists in list</returns>
    public bool ContainsIndex(int index);
    
    /// <summary>
    /// Clear list
    /// </summary>
    public void Clear();
    
    /// <summary>
    /// Get item T at index
    /// </summary>
    /// <param name="index">index of item T to get</param>
    /// <returns>Item of type T at index</returns>
    public T Get(int index);
    
    /// <summary>
    /// Get list as a string array
    /// </summary>
    /// <returns>List cast to an array of strings</returns>
    public string[] ToStringArray();
    
    /// <summary>
    /// Get list as a list of strings
    /// </summary>
    /// <returns>List cast to a list of strings</returns>
    public List<string> ToStringList();

    /// <summary>
    /// Sorts the list
    /// </summary>
    /// <param name="comparer">Comparer to sort by</param>
    public void Sort(IComparer<T> comparer);
}