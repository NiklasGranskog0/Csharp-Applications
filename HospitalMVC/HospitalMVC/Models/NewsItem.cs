namespace HospitalMVC.Models;

public class NewsItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Url { get; set; }
    public DateTime Date { get; set; }
}