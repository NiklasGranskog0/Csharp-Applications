using HospitalMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace HospitalMVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NavigationContentController(AppDbContext context) : Controller
{
    [HttpGet("{category}")]
    public async Task<IActionResult> GetContent(string category)
    {
        var content = context.NavigationContents.FirstOrDefault(nc => nc.Category == category);

        if (content == null) return NotFound();

        return Ok(new { content.Content });
    }
}