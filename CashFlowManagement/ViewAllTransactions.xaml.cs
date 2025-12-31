using System.Windows;
using System.Windows.Controls;
using CashFlowManagement.Enums;
using CashFlowManagement.Extensions;
using CashFlowManagement.Records;

namespace CashFlowManagement;

public partial class ViewAllTransactions : Window
{
    private List<Transaction> m_AllTransactions;

    /// <summary>
    /// Interaction logic for ViewAllTransactions.xaml
    /// </summary>
    /// <param name="transactions">Current list of all transactions</param>
    public ViewAllTransactions(List<Transaction> transactions)
    {
        InitializeComponent();

        m_AllTransactions = transactions;

        foreach (Transaction transaction in m_AllTransactions)
        {
            AllTransactions.Items.Add(transaction);
        }

        FilterComboBox.SetComboBoxEnum<FilterTypes>();
        SearchTypeComboBox.SetComboBoxEnum<SearchType>();

        DatePickerSearch.Visibility = Visibility.Visible;

        SearchTypeComboBox.SelectionChanged += SwitchSearchType;
        FilterComboBox.SelectionChanged += SwitchFilter;

        DatePickerSearch.SelectedDateChanged += SearchByMonth;
        CategorySearch.TextChanged += SearchByTypeText;
        DescriptionSearch.TextChanged += SearchByDescriptionText;
    }

    /// <summary>
    /// Lists all transactions where its description text matches the text searched for 
    /// </summary>
    private void SearchByDescriptionText(object sender, TextChangedEventArgs e)
    {
        ClearWindow();

        foreach (Transaction transaction in m_AllTransactions.Where(transaction =>
                     transaction.Description.Contains(DescriptionSearch.Text, StringComparison.OrdinalIgnoreCase)))
        {
            AllTransactions.Items.Add(transaction);
        }
    }

    /// <summary>
    /// Lists all transactions where its category name text matches the text searched for
    /// </summary>
    private void SearchByTypeText(object sender, TextChangedEventArgs e)
    {
        ClearWindow();

        foreach (Transaction transaction in m_AllTransactions.Where(transaction =>
                     transaction.Category.Name.Contains(CategorySearch.Text, StringComparison.OrdinalIgnoreCase)))
        {
            AllTransactions.Items.Add(transaction);
        }
    }

    /// <summary>
    /// Lists all transactions where its month is the same as selected in date picker 
    /// </summary>
    private void SearchByMonth(object? sender, SelectionChangedEventArgs e)
    {
        ClearWindow();

        foreach (Transaction transaction in m_AllTransactions.Where(transaction =>
                     transaction.Date.Month == DatePickerSearch.SelectedDate.Value.Month))
        {
            AllTransactions.Items.Add(transaction);
        }
    }

    /// <summary>
    /// When changing the type of search style show different input types
    /// </summary>
    private void SwitchSearchType(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
    {
        DatePickerSearch.Visibility = Visibility.Hidden;
        CategorySearch.Visibility = Visibility.Hidden;
        DescriptionSearch.Visibility = Visibility.Hidden;

        switch (SearchTypeComboBox.SelectedIndex)
        {
            case 0:
                DatePickerSearch.Visibility = Visibility.Visible;
                break;
            case 1:
                CategorySearch.Visibility = Visibility.Visible;
                break;
            case 2:
                DescriptionSearch.Visibility = Visibility.Visible;
                break;
        }
    }

    /// <summary>
    /// When changing the filter, lists all transactions by the filter selected
    /// </summary>
    private void SwitchFilter(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
    {
        ClearWindow();

        switch (FilterComboBox.SelectedIndex)
        {
            case 0: // All
                foreach (Transaction transaction in m_AllTransactions)
                {
                    AllTransactions.Items.Add(transaction);
                }

                break;
            case 1: // expense
                foreach (Transaction transaction in m_AllTransactions.Where(transaction =>
                             transaction.Category.Type.Equals(CategoryType.Expense)))
                {
                    AllTransactions.Items.Add(transaction);
                }

                break;
            case 2: // Revenue
                foreach (Transaction transaction in m_AllTransactions.Where(transaction =>
                             transaction.Category.Type.Equals(CategoryType.Revenue)))
                {
                    AllTransactions.Items.Add(transaction);
                }

                break;
            case 3: case 4: case 5: case 6: case 7: case 8: case 9: 
            case 10: case 11: case 12: case 13: case 14:
                if (Enum.TryParse(FilterComboBox.SelectedValue.ToString(), true, out Month month)) SearchByMonth(month);
                break;
        }
    }

    /// <summary>
    /// Searches for transactions by month
    /// </summary>
    /// <param name="month">Month to search for transactions in</param>
    private void SearchByMonth(Month month)
    {
        foreach (Transaction transaction in m_AllTransactions.Where(transaction =>
                     transaction.Date.Month == (int)month))
        {
            AllTransactions.Items.Add(transaction);
        }
    }

    /// <summary>
    /// Closes window on close button 
    /// </summary>
    private void CloseWindow_Click(object sender, RoutedEventArgs e) => Close();

    /// <summary>
    /// Clears list with all transactions
    /// </summary>
    private void ClearWindow() => AllTransactions.Items.Clear();
}