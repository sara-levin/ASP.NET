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

namespace NewStart.Pages.Mythologies
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        public Mythology mythology { get; set; }
        private NewStartDbContext database;

        public DeleteModel(NewStartDbContext database)
        {
            this.database = database;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            mythology = await database.Mythologies.FirstOrDefaultAsync(u => u.ID == id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var dbMythology = await database.Mythologies.FindAsync(id);
            database.Mythologies.Remove(dbMythology);

            await database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
