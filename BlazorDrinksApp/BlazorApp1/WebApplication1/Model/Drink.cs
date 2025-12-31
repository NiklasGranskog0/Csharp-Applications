using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model;

public class Drink
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Please enter a drink name")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please enter a description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Please enter a category")]
    public string? Category { get; set; }

    [Required(ErrorMessage = "Please enter a type")]
    public string? Type { get; set; }

    [Required(ErrorMessage = "Please select an image")]
    public string? ImageUrl { get; set; }
}