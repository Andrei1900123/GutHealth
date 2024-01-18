using GutHealth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class FoodItemsModel : PageModel
{
    private readonly GutHealthDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public FoodItemsModel(GutHealthDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IList<FoodItem> FoodItemList { get; set; }

    public async Task OnGetAsync()
    {
        FoodItemList = await _context.FoodItems.ToListAsync();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(int foodItemId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == user.Id);
        if (cart == null)
        {
            cart = new Cart { UserId = user.Id };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.FoodItemId == foodItemId);

        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                CartId = cart.CartId,
                FoodItemId = foodItemId,
                Quantity = 1
            };
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity++;
        }

        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}
