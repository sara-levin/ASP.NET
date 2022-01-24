using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewStart.Data;
using NewStart.Models;

namespace NewStart.Pages.Mythologies
{
    public class DetailsModel : PageModel
    {
        public Mythology mythology { get; set; }
        private NewStartDbContext database;

        public DetailsModel(NewStartDbContext database)
        {
            this.database = database;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            mythology = await database.Mythologies.FirstOrDefaultAsync(u => u.ID == id);

            return Page();
        }
    }
}