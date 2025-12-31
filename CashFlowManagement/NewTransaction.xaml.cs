using System.Windows;
using CashFlowManagement.Enums;
using CashFlowManagement.Records;
using static CashFlowManagement.Extensions.Extension;

namespace CashFlowManagement;

public partial class NewTransaction : Window
{
    public event Action<Transaction> TransactionAdded;

    /// <summary>
    /// Interaction logic for NewTransaction.xaml
    /// </summary>
    public NewTransaction()
    {
        InitializeComponent();

        BusinessComboBox.SetComboBoxEnum<Business>();
        CategoryTypeComboBox.SetComboBoxEnum<CategoryType>();
    }

    /// <summary>
    /// Creates a new Transactions when clicking on the Add Transaction button 
    /// </summary>
    private void AddTransaction_Click(object sender, RoutedEventArgs e)
    {
        if (DatePicker.SelectedDate is null)
        {
            MessageBox("Please select a date!");
            return;
        }

        if (string.IsNullOrEmpty(CategoryNameBox.Text))
        {
            MessageBox("Please enter a category name!");
            return;
        }

        if (!decimal.TryParse(AmountTextBox.Text, out decimal dec))
        {
            MessageBox("Please enter a valid amount!");
            return;
        }

        if (string.IsNullOrEmpty(DescriptionBox.Text))
        {
            MessageBox("Please enter a description!");
            return;
        }

        Transaction transaction = new Transaction
        {
            Date = DatePicker.SelectedDate.Value,
            Amount = dec,

            Category = new Category
            {
                Name = CategoryNameBox.Text,
                Type = (CategoryType)CategoryTypeComboBox.SelectedValue,
                UnitType = (Unit)BusinessComboBox.SelectedValue
            },

            Description = DescriptionBox.Text
        };

        SendTransaction(transaction);
        MessageBox("Added transaction successfully!", "New Transaction", MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    /// <summary>
    /// Sends added transactions to main window
    /// </summary>
    /// <param name="transaction">The transaction that is sent</param>
    private void SendTransaction(Transaction transaction) => TransactionAdded.Invoke(transaction);
}