using WPFLibrary.Enums;

namespace WPFLibrary.Record_Objects;

public record GameRecord
{
    public string? Title { get; init; }
    public string? Publisher { get; init; }
    public Genre Genre { get; init; }
    public string? Developer { get; init; }
    public Rating Rating { get; init; }
    public Guid Guid { get; init; }
}