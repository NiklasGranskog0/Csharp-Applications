using WPFLibrary.Enums;

namespace WPFLibrary.Record_Objects;

public record MovieRecord
{
    public string? Title { get; init; }
    public string? MovieActor { get; init; }
    public Genre Genre { get; init; }
    public int Duration { get; init; }
    public Rating Rating { get; init; }
    public Guid Guid { get; init; }
}