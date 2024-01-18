namespace GutHealth.Models;
using System;
using System.Collections.Generic;

public class Order
{
    public int OrderId { get; set; }
    public string? UserId { get; set; } // Link to ApplicationUser
    public DateTime OrderDate { get; set; }

    // Navigation properties
    
    public ApplicationUser? User { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
}
