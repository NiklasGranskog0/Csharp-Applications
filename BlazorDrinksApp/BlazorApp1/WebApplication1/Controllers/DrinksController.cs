using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrinksController(DrinksDbContext context) : ControllerBase
{
    [HttpPost]
    public ActionResult<Drink> CreateDrink(Drink drink)
    {
        drink.Id = Guid.NewGuid();
        context.Drinks.Add(drink);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetDrinkById), new { id = drink.Id }, drink);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Drink>> GetAllDrinks()
    {
        return context.Drinks.ToList();
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Drink> GetDrinkById(Guid id)
    {
        var drink = context.Drinks.FirstOrDefault(x => x.Id == id);
        return drink != null ? Ok(drink) : NotFound();
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateDrink(Guid id, Drink drink)
    {
        var drinkToUpdate = context.Drinks.FirstOrDefault(x => x.Id == id);
        if (drinkToUpdate != null)
        {
            drinkToUpdate.Name = drink.Name;
            drinkToUpdate.Description = drink.Description;
            drinkToUpdate.Category = drink.Category;
            drinkToUpdate.Type = drink.Type;
            drinkToUpdate.ImageUrl = drink.ImageUrl;
            context.SaveChanges();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteDrink(Guid id)
    {
        var drink = context.Drinks.FirstOrDefault(x => x.Id == id);
        if (drink != null)
        {
            context.Drinks.Remove(drink);
            context.SaveChanges();
        }

        return NoContent();
    }
}