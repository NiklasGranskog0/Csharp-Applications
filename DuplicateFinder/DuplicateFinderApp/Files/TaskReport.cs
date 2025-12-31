namespace DuplicateFinderApp.Files;

public struct TaskReport
{
    public bool Success;
    public string Message;
    public string? Value;

    public void Report(bool success, string message, string? value = null)
    {
        Success = success;
        Message = message;
        Value = value;
    }

    public void Clear()
    {
        Success = false;
        Message = string.Empty;
        Value = null;
    }
}