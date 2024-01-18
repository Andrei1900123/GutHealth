using System.ComponentModel.DataAnnotations;

namespace GutHealth.Models;
public class FoodItem
{
    public int FoodItemId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    [Display(Name = "Category")]
    public int FoodCategoryId { get; set; }

    // Navigation property
    [Display(Name = "Category")]
    public FoodCategory? FoodCategory { get; set; }
}

