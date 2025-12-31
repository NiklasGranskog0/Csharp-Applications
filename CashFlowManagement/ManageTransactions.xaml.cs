using System.Windows;
using System.Windows.Controls;
using CashFlowManagement.Records;
using static CashFlowManagement.Extensions.Extension;

namespace CashFlowManagement;

public partial class ManageTransactions : Window
{
    private readonly List<Transaction> m_AllTransactions;
    public event Action<List<Transaction>> OnTransactionsChanged;
    
    /// <summary>
    /// Interaction logic for NewTransaction.xaml
    /// </summary>
    /// <param name="transactions">Current list of transactions</param>
    public ManageTransactions(List<Transaction> transactions)
    {
        m_AllTransactions = transactions;
        InitializeComponent();

        AllTransactions.SelectionMode = SelectionMode.Single;
        
        foreach (Transaction transaction in m_AllTransactions)
        {
            AllTransactions.Items.Add(transaction);
        }
    }

    /// <summary>
    /// Closes the window when clicking on the close button
    /// </summary>
    private void CloseWindow_Click(object sender, RoutedEventArgs e) => Close();

    /// <summary>
    /// Removes selected transactions that is selected 
    /// </summary>
    private void RemoveSelectedItem_Click(object sender, RoutedEventArgs e)
    {
        if (AllTransactions.SelectedIndex == -1)
        {
            MessageBox("Please select a transaction to remove.");
            return;
        }

        int index = AllTransactions.SelectedIndex;
        
        AllTransactions.Items.RemoveAt(index);
        m_AllTransactions.RemoveAt(index);
        OnTransactionsChanged.Invoke(m_AllTransactions);
    }
}