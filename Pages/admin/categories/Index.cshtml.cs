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
    public class IndexModel : PageModel
    {
        private readonly GutHealthDbContext _context;

        public IndexModel(GutHealthDbContext context)
        {
            _context = context;
        }

        public IList<FoodCategory> FoodCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            FoodCategory = await _context.FoodCategories.ToListAsync();
        }
    }
}
