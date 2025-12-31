using WPFLibrary.Enums;

namespace WPFLibrary.Record_Objects;

public record BookRecord
{
    public string? Title { get; init; }
    public string? Author { get; init; }
    public Genre Genre { get; init; }
    public int Pages { get; init; }
    public Rating Rating { get; init; }
    public Guid Guid { get; init; }
}