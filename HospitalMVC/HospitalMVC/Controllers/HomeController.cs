using System.Diagnostics;
using HospitalMVC.Data;
using Microsoft.AspNetCore.Mvc;
using HospitalMVC.Models;
using HospitalMVC.ViewModels;

namespace HospitalMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext m_Context;
    
    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        m_Context = context;
        _logger = logger;
        
        NavigationContentSeeder.Seed(m_Context);
    }

    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel
        {
            Patients = m_Context.Patients.Count(),
            Doctors = m_Context.Doctors.Count(),
            WaitTime = m_Context.WaitTime(),
            Notification = m_Context.Notification,
            NewsItems = m_Context.NewsItems.ToList()
        };
        
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}