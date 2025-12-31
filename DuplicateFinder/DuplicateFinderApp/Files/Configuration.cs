namespace DuplicateFinderApp.Files;

public struct Configuration
{
    public string? FolderPath;

    // Attributes
    public bool? Name;
    public bool? LastWriteTime;
    public bool? CreatedDate;
    public bool? Size;
    // public bool? Hash;

    // File types
    public bool? AllFiles;
    public bool? Pictures;
    public bool? Pdf;
    public bool? TextFiles;

    public HashSet<string> GetFileExtensions()
    {
        var newSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (Pictures == true) newSet.UnionWith([".png", ".jpg", ".jpeg"]);
        if (Pdf == true) newSet.Add(".pdf");
        if (TextFiles == true) newSet.Add(".txt");

        return newSet;
    }

    public bool NoFileTypesSelected()
    {
        return AllFiles == false && Pictures == false && Pdf == false && TextFiles == false;
    }

    public bool NoAttributeSelected()
    {
        return Name == false && LastWriteTime == false && CreatedDate == false && Size == false /*&& Hash == false*/;
    }
}

public static class ConfigurationExtensions
{
    public static string GetUniqueKey(this Configuration config, FileStruct fileStruct)
    {
        var uniqueKey = "";

        if (config.Name == true)
        {
            var fileName = fileStruct.FileName;

            // Trim file extension
            if (fileStruct.FileName!.Contains('.'))
                fileName = fileStruct.FileName[..fileStruct.FileName.IndexOf('.')];

            uniqueKey += fileName + "|";
        }

        if (config.Size == true) uniqueKey += fileStruct.Size + "|";
        if (config.LastWriteTime == true) uniqueKey += fileStruct.LastWriteTime + "|";
        if (config.CreatedDate == true) uniqueKey += fileStruct.CreatedDate + "|";

        return uniqueKey;
    }
}