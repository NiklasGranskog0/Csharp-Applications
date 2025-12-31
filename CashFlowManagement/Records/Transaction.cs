namespace CashFlowManagement.Records;

public record Transaction
{
    public required DateTime Date { get; init; }
    public required decimal Amount { get; init; }
    public required Category Category { get; init; }
    public required string Description { get; init; }

    public override string ToString()
    {
        return $"Date: {Date.ToShortDateString()}, Amount: {Amount}, Category: {Category}, Description: {Description}";
    }
}