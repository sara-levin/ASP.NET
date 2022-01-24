using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewStart.Models;

namespace NewStart.Pages.Units
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

        public IList<Unit> Units { get; set; }
        public IList<Mythology> Mythologies { get; set; }
        public async Task OnGetAsync()
        {
            var query = database.Units.Include(u => u.Mythology).AsNoTracking();

            if(SearchTerm !=null)
            {
                query = query.Where(u => u.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                                         u.Mythology.Name.ToLower().Contains(SearchTerm.ToLower()));
            }

            if (SortColumn !=null)
            {
                if(SortColumn == "Name")
                {
                    query = query.OrderBy(u => u.Name);
                }
                else if (SortColumn == "Class")
                {
                    query = query.OrderBy(u => u.Class);
                }
                else if (SortColumn == "Hitpoints")
                {
                    query = query.OrderByDescending(u => u.Hitpoints);
                }
                else if (SortColumn == "Attack")
                {
                    query = query.OrderByDescending(u => u.Attack);
                }
                else if (SortColumn == "Cost")
                {
                    query = query.OrderByDescending(u => u.Cost);
                }
                else if (SortColumn == "Mythology")
                {
                    query = query.OrderBy(u => u.Mythology.Name); 
                }
            }   

            Units = await query.ToListAsync();
        }
    }
}