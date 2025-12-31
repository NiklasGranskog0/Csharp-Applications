using HospitalMVC.Models;
using HospitalMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HospitalMVC.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<NewsItem> NewsItems { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<NavigationContent> NavigationContents { get; set; }

    public WaitTime WaitTime()
    {
        var docCount = Doctors.Count();
        var patCount = Patients.Count();

        if (docCount == 0) return patCount > 0 ? Models.WaitTime.Long : Models.WaitTime.Short;
        
        var ratio = (double)patCount / docCount;

        return ratio switch
        {
            <= 2 => Models.WaitTime.Short,
            <= 4 => Models.WaitTime.Medium,
            <= 10 => Models.WaitTime.Long,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public readonly string Notification = "None";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}