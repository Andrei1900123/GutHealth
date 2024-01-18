namespace GutHealth.Models;
using System.Collections.Generic;

public class Cart
{
    public int CartId { get; set; }
    public string? UserId { get; set; } // Link to ApplicationUser

    // Navigation properties
    public ApplicationUser? User { get; set; }
    public ICollection<CartItem>? CartItems { get; set; }
}
