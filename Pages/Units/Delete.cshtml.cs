using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewStart.Data;
using NewStart.Models;

namespace NewStart.Pages.Units
{
    [Authorize]

    public class DeleteModel : PageModel
    {
        public Unit unit { get; set; }
        private NewStartDbContext database;

        public DeleteModel(NewStartDbContext database)
        {
            this.database = database;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            unit = await database.Units.Include(unit => unit.Mythology).FirstOrDefaultAsync(u => u.ID == id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var dbUnit = await database.Units.FindAsync(id);
            database.Units.Remove(dbUnit);

            await database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
