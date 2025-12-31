namespace HospitalMVC.Models;

public enum WaitTime
{
    Short,
    Medium,
    Long,
}

public record Doctor(Guid Id, string Name, string IsAvailable);