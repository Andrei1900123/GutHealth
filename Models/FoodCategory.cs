
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace GutHealth.Models;

public class FoodCategory
{
    
    public int FoodCategoryId { get; set; }
    [Display(Name = "Category")]
    public string? Name { get; set; }

    // Navigation property
    public ICollection<FoodItem>? FoodItems { get; set; }
}
