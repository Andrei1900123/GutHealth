using GutHealth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class UserListModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserListModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IList<ApplicationUser> Users { get; set; }

    public async Task OnGetAsync()
    {
        Users = await _userManager.Users.ToListAsync();
    }
}
