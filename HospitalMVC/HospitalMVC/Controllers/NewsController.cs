using HospitalMVC.Data;
using HospitalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalMVC.Controllers;

public class NewsController(AppDbContext context) : Controller
{
    public IActionResult Index()
    {
        return View(context.NewsItems.ToList());
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    public IActionResult Delete(Guid id)
    {
        if (id == Guid.Empty) return NotFound();
        
        var item = context.NewsItems.Find(id);
        
        if (item is null) return NotFound();

        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (id == Guid.Empty) return NotFound();
        
        var item = await context.NewsItems.FindAsync(id);
        
        if (item is null) return NotFound();
        
        context.NewsItems.Remove(item);
        await context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        if (id == Guid.Empty) return NotFound();
        
        var item = await context.NewsItems.FindAsync(id);
        
        if (item is null) return NotFound();
        
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, NewsItem newsItem)
    {
        if (id != newsItem.Id) return NotFound();

        if (!ModelState.IsValid) return View(newsItem);
        
        try
        {
            context.Update(newsItem);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            if (!context.NewsItems.Any(n => n.Id == newsItem.Id)) return NotFound();
            throw;
        }
            
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NewsItem item)
    {
        if (ModelState.IsValid)
        {
            await context.NewsItems.AddAsync(item);
            await context.SaveChangesAsync();
        }
        
        return RedirectToAction(nameof(Index));
    }
}