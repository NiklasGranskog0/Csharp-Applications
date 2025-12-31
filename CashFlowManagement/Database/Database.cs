using System.IO;
using System.Text.Json;
using System.Windows;
using CashFlowManagement.Records;
using static CashFlowManagement.Extensions.Extension;

namespace CashFlowManagement.Database;

public static class Database
{
    public static void SetJsonOptions(JsonSerializerOptions jsonOptions) => s_jsonSerializerOptions = jsonOptions;

    private static JsonSerializerOptions s_jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };

    /// <summary>
    /// Saves a list of transactions to a json file
    /// </summary>
    /// <param name="transactions">List of transactions to be saved to a file</param>
    public static void SaveTransaction(List<Transaction> transactions)
    {
        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Transactions.json",
            JsonSerializer.Serialize(transactions, s_jsonSerializerOptions));
    }

    /// <summary>
    /// Reads a saved json file and reads it contents
    /// </summary>
    /// <returns>A list of transactions contained in the saved Json file</returns>
    public static List<Transaction> ReadDatabase()
    {
        List<Transaction> transactions = [];
        string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Transactions.json");

        if (string.IsNullOrEmpty(json) || json.Length < 10)
        {
            MessageBox("No transactions found", "Database");
            return transactions;
        }

        try
        {
            transactions = JsonSerializer.Deserialize<List<Transaction>>(json, s_jsonSerializerOptions);
            MessageBox("Loaded Transactions", "Database", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return transactions;
    }
}