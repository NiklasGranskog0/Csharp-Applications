using System.Windows;
using System.Windows.Controls;
using CashFlowManagement.Enums;
using CashFlowManagement.Records;
using static System.Windows.MessageBox;

namespace CashFlowManagement.Extensions;

public static class Extension
{
    public static void SetComboBoxEnum<T>(this ComboBox comboBox, int selectedIndex = 0) where T : Enum
    {
        comboBox.ItemsSource = Enum.GetValues(typeof(T));
        comboBox.SelectedIndex = selectedIndex;
        comboBox.IsReadOnly = true;
    }
    
    /// <summary>
    /// Calculates the decimal amount from a list of transactions
    /// </summary>
    /// <param name="transactions">The list of transactions to calculate</param>
    /// <returns>Revenue - Expenses from the list of transactions</returns>
    public static decimal CalculateCashNetFlow(List<Transaction> transactions)
    {
        decimal expenses = 0;
        decimal revenue = 0;

        foreach (var transaction in transactions)
        {
            if (transaction.Category.Type.Equals(CategoryType.Expense)) expenses += transaction.Amount;
            if (transaction.Category.Type.Equals(CategoryType.Revenue)) revenue += transaction.Amount;
        }

        return revenue - expenses;
    }
    
    public static void MessageBox(string text, string caption = "Error", MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage image = MessageBoxImage.Error)
    {
        Show(text, caption, button, image);
    }
}