using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GutHealth.Models;
using Microsoft.AspNetCore.Authorization;

namespace GutHealth.Pages.admin.items
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly GutHealthDbContext _context;

        public IndexModel(GutHealthDbContext context)
        {
            _context = context;
        }

        public IList<FoodItem> FoodItem { get; set; } = default!;

        public async Task OnGetAsync()
        {
            FoodItem = await _context.FoodItems
                .Include(f => f.FoodCategory).ToListAsync();
        }
    }
}
