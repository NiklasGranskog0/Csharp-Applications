using HospitalMVC.Models;

namespace HospitalMVC.ViewModels;

public class HomeIndexViewModel
{
    public int Patients { get; set; }
    public int Doctors { get; set; }
    public WaitTime WaitTime { get; set; }
    public string Notification { get; set; } = "None";
    public List<NewsItem> NewsItems { get; set; }
}