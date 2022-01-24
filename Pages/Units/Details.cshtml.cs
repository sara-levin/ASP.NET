using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewStart.Data;
using NewStart.Models;

namespace NewStart.Pages.Units
{
    public class DetailsModel : PageModel
    {
        private NewStartDbContext database;

        public DetailsModel(NewStartDbContext database)
        {
            this.database = database;
        }

        public Unit unit { get; set; }


        public async Task <IActionResult> OnGetAsync(int id)
        {
            unit = await database.Units.Include(unit => unit.Mythology).FirstOrDefaultAsync(u => u.ID == id);

            return Page();
        }
    }
}
