using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GutHealth.Models;
using Microsoft.AspNetCore.Authorization;

namespace GutHealth.Pages.admin.categories
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly GutHealthDbContext _context;

        public DetailsModel(GutHealthDbContext context)
        {
            _context = context;
        }

        public FoodCategory FoodCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodcategory = await _context.FoodCategories.FirstOrDefaultAsync(m => m.FoodCategoryId == id);
            if (foodcategory == null)
            {
                return NotFound();
            }
            else
            {
                FoodCategory = foodcategory;
            }
            return Page();
        }
    }
}
