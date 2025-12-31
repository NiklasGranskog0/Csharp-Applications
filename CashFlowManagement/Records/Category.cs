using CashFlowManagement.Enums;

namespace CashFlowManagement.Records;

public record Category
{
    public required string Name { get; init; }
    public required CategoryType Type { get; init; }
    public required Unit UnitType { get; init; }

    public override string ToString()
    {
        return $"[{Type}, {Name}, {UnitType}]";
    }
}