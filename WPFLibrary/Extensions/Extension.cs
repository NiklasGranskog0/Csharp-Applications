using System.Windows.Controls;
using WPFLibrary.Enums;

namespace WPFLibrary.Extensions;

public static class Extension
{
    /// <summary>
    /// Binds a combobox to an enum type
    /// </summary>
    /// <param name="comboBox">The combobox</param>
    /// <param name="selectedIndex">First value on startup</param>
    /// <typeparam name="T">Enum type</typeparam>
    public static void SetComboBoxEnum<T>(this ComboBox comboBox, int selectedIndex = 0) where T : Enum
    {
        comboBox.ItemsSource = Enum.GetValues(typeof(T));
        comboBox.SelectedIndex = selectedIndex;
        comboBox.IsReadOnly = true;
    }

    /// <summary>
    /// Updates an items name in a listbox
    /// </summary>
    /// <param name="listBox">The Listbox</param>
    /// <param name="index">Index of the item</param>
    /// <param name="itemName">The name to set it to</param>
    public static void UpdateItemNameInList(this ListBox listBox, int index, string itemName)
        => listBox.Items[index] = itemName;

    /// <summary>
    /// Adds an item name to the listbox
    /// </summary>
    /// <param name="listBox"> The listbox that will add the item </param>
    /// <param name="name"> The name of the item </param>
    public static void AddItemNameToList(this ListBox listBox, string name) => listBox.Items.Add(name);

    /// <summary>
    /// If name is longer than 15 characters set last 3 characters to '...'
    /// </summary>
    /// <param name="name"></param>
    /// <param name="rating"></param>
    /// <returns>string with the new name</returns>
    public static string CreateNameForItem(string name, Rating rating)
    {
        if (name.Length > 15)
        {
            name = name[..12] + "...";
        }

        return $"{name} : {rating.ToString()}";
    }
}