namespace GutHealth.Models;
public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int FoodItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; } // Price at the time of order

    // Navigation properties
    public Order? Order { get; set; }
    public FoodItem? FoodItem { get; set; }
}

