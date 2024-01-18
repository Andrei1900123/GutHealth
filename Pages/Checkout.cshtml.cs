using GutHealth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CheckoutModel : PageModel
{
    private readonly GutHealthDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CheckoutModel(GutHealthDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IList<CartItem> CartItems { get; set; }
    public decimal OrderTotal { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.FoodItem)
            .FirstOrDefaultAsync(c => c.UserId == user.Id);

        CartItems = cart?.CartItems.ToList() ?? new List<CartItem>();
        OrderTotal = CartItems.Sum(ci => ci.Quantity * ci.FoodItem.Price);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.FoodItem)
            .FirstOrDefaultAsync(c => c.UserId == user.Id);

        if (cart == null || !cart.CartItems.Any())
        {
            return RedirectToPage("/Cart");
        }

        // Create an order from the cart items
        var order = new Order
        {
            UserId = user.Id,
            OrderDate = DateTime.UtcNow,
            OrderItems = cart.CartItems.Select(ci => new OrderItem
            {
                FoodItemId = ci.FoodItemId,
                Quantity = ci.Quantity,
                Price = ci.FoodItem.Price
            }).ToList()
        };

        _context.Orders.Add(order);

        // Clear the cart
        _context.CartItems.RemoveRange(cart.CartItems);

        await _context.SaveChangesAsync();

        return RedirectToPage("/OrderHistory");
    }
}
