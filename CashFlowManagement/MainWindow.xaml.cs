using System.Windows;
using System.Windows.Controls;
using CashFlowManagement.Enums;
using CashFlowManagement.Extensions;
using CashFlowManagement.Records;
using static CashFlowManagement.Database.Database;

namespace CashFlowManagement;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<Transaction> m_Transactions = [];
    private readonly Dictionary<Unit, List<Transaction>> m_TransactionByUnit = [];

    private NewTransaction m_NewTransactionWindow;
    private ViewAllTransactions m_ViewAllTransactions;
    private ManageTransactions m_ManageTransactions;

    public MainWindow()
    {
        InitializeComponent();
        ProfitTextBox.Text = "0";
        SurplusTextBox.Text = "0";

        MonthComboBox.SetComboBoxEnum<Month>();
        MonthComboBox.SelectionChanged += MonthComboBoxOnSelectionChanged;
        MonthComboBox.SelectionChanged += MonthlyTopExpensesAndRevenues;
    }

    /// <summary>
    /// When changing the month, update list boxes with that month's expenses & revenues then calculate surplus/deficit.
    /// </summary>
    private void MonthComboBoxOnSelectionChanged(object sender, SelectionChangedEventArgs? e)
    {
        ClearMonthlyTransactions();

        if (m_TransactionByUnit.TryGetValue(Unit.Business, out List<Transaction> businessTransactions))
        {
            if (businessTransactions.Count < 1) return;

            List<Transaction> transactionsToCalculate = [];
            foreach (Transaction transaction in businessTransactions.Where(transaction =>
                         transaction.Date.Month == MonthComboBox.SelectedIndex + 1))
            {
                BusinessesListBox.Items.Add($"{transaction.Category.Type}[{transaction.Amount}]");
                transactionsToCalculate.Add(transaction);
            }

            if (BusinessesListBox.Items.Count < 1) return;

            decimal cashNetFlow = Extension.CalculateCashNetFlow(transactionsToCalculate);

            if (cashNetFlow > 0)
            {
                ProfitTextBox.Text = cashNetFlow.ToString();
                ProfitLossLabel.Content = "Profit";
            }
            else
            {
                ProfitTextBox.Text = cashNetFlow.ToString();
                ProfitLossLabel.Content = "Loss";
            }
        }

        if (m_TransactionByUnit.TryGetValue(Unit.Individual, out List<Transaction> individualTransactions))
        {
            if (individualTransactions.Count < 1) return;

            List<Transaction> transactionsToCalculate = [];
            foreach (Transaction transaction in individualTransactions.Where(transaction =>
                         transaction.Date.Month == MonthComboBox.SelectedIndex + 1))
            {
                IndividualsListBox.Items.Add($"{transaction.Category.Type}[{transaction.Amount}]");
                transactionsToCalculate.Add(transaction);
            }

            if (IndividualsListBox.Items.Count < 1) return;

            decimal cashNetFlow = Extension.CalculateCashNetFlow(transactionsToCalculate);

            if (cashNetFlow > 0)
            {
                SurplusTextBox.Text = cashNetFlow.ToString();
                SurplusLabel.Content = "Surplus";
            }
            else
            {
                SurplusTextBox.Text = cashNetFlow.ToString();
                SurplusLabel.Content = "Deficit";
            }
        }
    }

    /// <summary>
    /// Updates top expenses and revenues of selected month
    /// </summary>
    private void MonthlyTopExpensesAndRevenues(object sender, SelectionChangedEventArgs? selectionChangedEventArgs)
    {
        ClearMonthlyTopTransactions();
        int numberOfTop = 3;
        
        if (m_Transactions.Count > 1)
        {
            m_Transactions.Sort((x, y) => x.Amount.CompareTo(y.Amount));
            m_Transactions.Reverse();
        }

        List<Transaction> topExpenses =
            m_Transactions.Where(t =>
                    t.Category.Type == CategoryType.Expense && t.Date.Month == MonthComboBox.SelectedIndex + 1)
                .ToList();

        List<Transaction> topRevenues =
            m_Transactions.Where(t =>
                    t.Category.Type == CategoryType.Revenue && t.Date.Month == MonthComboBox.SelectedIndex + 1)
                .ToList();

        if (numberOfTop > topExpenses.Count) numberOfTop = topExpenses.Count;
        foreach (Transaction transaction in topExpenses[..numberOfTop])
        {
            TopExpenses.Items.Add($"{transaction.Category.Name} ({transaction.Amount})");
        }

        if (numberOfTop > topRevenues.Count) numberOfTop = topRevenues.Count;
        foreach (Transaction transaction in topRevenues[..numberOfTop])
        {
            TopRevenue.Items.Add($"{transaction.Category.Name} ({transaction.Amount})");
        }

        TotalNetCashFlow.Content =
            $"Total Net-Cash Flow: {Extension.CalculateCashNetFlow(m_Transactions.Where(t => t.Date.Month == MonthComboBox.SelectedIndex + 1).ToList())}";
    }

    /// <summary>
    /// Adds a transaction to local list of transactions
    /// </summary>
    /// <param name="transaction">Transaction to add</param>
    private void AddTransaction(Transaction transaction)
    {
        m_Transactions.Add(transaction);
        OnTransactionsUpdated();
    }

    /// <summary>
    /// When clicking on Add Transactions button
    /// Opens a new window that can create new transactions
    /// </summary>
    private void AddTransaction_Click(object sender, RoutedEventArgs e)
    {
        m_NewTransactionWindow = new NewTransaction();
        m_NewTransactionWindow.TransactionAdded += AddTransaction;
        m_NewTransactionWindow.Show();
    }

    /// <summary>
    /// When clicking on view transactions
    /// Opens a new window that contains a list of all transactions 
    /// </summary>
    private void ViewAllTransactions_Click(object sender, RoutedEventArgs e)
    {
        m_ViewAllTransactions = new ViewAllTransactions(m_Transactions);
        m_ViewAllTransactions.Show();
    }

    /// <summary>
    /// Saves all transactions to a file 
    /// </summary>
    private void SaveTransactions_Click(object sender, RoutedEventArgs e) => SaveTransaction(m_Transactions);

    /// <summary>
    /// Loads transactions from a file, adds them to the local list of transactions
    /// </summary>
    private void LoadTransactions_Click(object sender, RoutedEventArgs e)
    {
        m_Transactions = ReadDatabase();

        OnTransactionsUpdated();
    }

    /// <summary>
    /// Clears list boxes of monthly report
    /// </summary>
    private void ClearMonthlyTransactions()
    {
        BusinessesListBox.Items.Clear();
        IndividualsListBox.Items.Clear();
        ProfitTextBox.Text = "0";
        SurplusTextBox.Text = "0";
    }

    /// <summary>
    /// Clears list boxes of monthly top report
    /// </summary>
    private void ClearMonthlyTopTransactions()
    {
        TopExpenses.Items.Clear();
        TopRevenue.Items.Clear();
    }

    /// <summary>
    /// When clicking on Manage Transactions button
    /// Open a new window that has a list of all transactions where they can be managed
    /// </summary>
    private void ManageTransactions_Click(object sender, RoutedEventArgs e)
    {
        if (m_Transactions.Count < 1)
        {
            MessageBox.Show("There are no transactions to manage.", "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);
            return;
        }

        m_ManageTransactions = new ManageTransactions(m_Transactions);
        m_ManageTransactions.OnTransactionsChanged += ManageTransactionsOnOnTransactionsChanged;
        m_ManageTransactions.Show();
    }

    /// <summary>
    /// When a transactions have been "managed" get the new managed list of transactions 
    /// </summary>
    /// <param name="obj">The new list of transactions</param>
    private void ManageTransactionsOnOnTransactionsChanged(List<Transaction> obj)
    {
        m_Transactions = obj;

        OnTransactionsUpdated();
    }

    /// <summary>
    /// When the transactions list has received an update, update monthly report list boxes 
    /// </summary>
    private void OnTransactionsUpdated()
    {
        m_TransactionByUnit.Clear();

        List<Transaction> businessTransactions =
            m_Transactions.Where(transaction => transaction.Category.UnitType.Equals(Unit.Business)).ToList();
        List<Transaction> individualTransactions = m_Transactions
            .Where(transaction => transaction.Category.UnitType.Equals(Unit.Individual)).ToList();

        m_TransactionByUnit.TryAdd(Unit.Business, businessTransactions);
        m_TransactionByUnit.TryAdd(Unit.Individual, individualTransactions);

        MonthComboBoxOnSelectionChanged(null!, null);
        MonthlyTopExpensesAndRevenues(null!, null);
    }
}