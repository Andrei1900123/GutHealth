using GutHealth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize(Roles = "Admin")]
public class DeleteUserModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteUserModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public ApplicationUser User { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        User = await _userManager.FindByIdAsync(id);

        if (User == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userToDelete = await _userManager.FindByIdAsync(User.Id);

        if (userToDelete == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(userToDelete);

        if (!result.Succeeded)
        {
            // Handle the error, display to the user
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        return RedirectToPage("./UserList");
    }
}

