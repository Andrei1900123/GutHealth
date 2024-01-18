using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GutHealth.Models;
using Microsoft.AspNetCore.Authorization;

namespace GutHealth.Pages.admin.items
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly GutHealthDbContext _context;

        public CreateModel(GutHealthDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["FoodCategoryId"] = new SelectList(_context.FoodCategories, "FoodCategoryId", "Name");
            return Page();
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FoodItems.Add(FoodItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
