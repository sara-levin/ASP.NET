using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewStart.Models;

namespace NewStart.Pages.Mythologies
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

        public IList<Mythology> Mythologies { get; set; }
        public async Task OnGetAsync()
        {
            var query = database.Mythologies.AsNoTracking();

            if (SearchTerm != null)
            {
                query = query.Where(m => m.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                                         m.OrginCountry.ToLower().Contains(SearchTerm.ToLower()));
            }

            if (SortColumn != null)
            {
                if (SortColumn == "Name")
                {
                    query = query.OrderBy(m => m.Name);
                }
                else if (SortColumn == "Country of orgin")
                {
                    query = query.OrderBy(m => m.OrginCountry);
                }
            }
            Mythologies = await query.ToListAsync();
        }
    }
}
