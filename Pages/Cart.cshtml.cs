using GutHealth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
public class CartModel : PageModel
{
    private readonly GutHealthDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartModel(GutHealthDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IList<CartItem> CartItems { get; set; }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.FoodItem)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            CartItems = cart?.CartItems.ToList() ?? new List<CartItem>();
        }
    }

    public async Task<IActionResult> OnPostRemoveFromCartAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUpdateQuantityAsync(int cartItemId, int quantity)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem != null && quantity > 0)
        {
            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();
        }

        return RedirectToPage();
    }


}
