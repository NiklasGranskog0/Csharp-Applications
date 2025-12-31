namespace DuplicateFinderApp.Files;

public struct FileStruct
{
    public string? FileName { get; set; }
    public string? FullPath { get; set; }
    public long Size { get; set; }
    public DateTime LastWriteTime { get; set; }
    public DateTime CreatedDate { get; set; }

    public string SizeMb => $"{(double)Size / (1024 * 1024):N2} MB";
}