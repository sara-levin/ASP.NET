using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewStart.Models;

namespace NewStart.Pages.Gods
{
    public class IndexModel : PageModel
    {
        private readonly NewStart.Data.NewStartDbContext database;

        public IndexModel(NewStart.Data.NewStartDbContext context)
        {
            database = context;
        }

        [FromQuery]
        public string SearchTerm { get; set; }
        [FromQuery]
        public string SortColumn { get; set; }

        public IList<God> Gods { get; set; }
        public async Task OnGetAsync()
        {
            var query = database.Gods.Include(g => g.Mythology).AsNoTracking();

            if (SearchTerm != null)
            {
                query = query.Where(g => g.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                                         g.Focus.ToLower().Contains(SearchTerm.ToLower()) ||
                                         g.Mythology.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                                         g.Age.ToLower().Contains(SearchTerm.ToLower()));
            }


            if (SortColumn != null)
            {
                if (SortColumn == "Name") { query = query.OrderBy(m => m.Name); }
                else if (SortColumn == "Age") { query = query.OrderBy(g => g.Age); }
                else if (SortColumn == "Focus") { query = query.OrderBy(g => g.Focus); }
                else { query = query.OrderBy(g => g.Mythology.Name); }
            }
            Gods = await query.ToListAsync();
        }
    }
}