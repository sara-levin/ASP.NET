using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewStart.Data;
using NewStart.Models;

namespace NewStart.Pages.Mythologies
{
    [Authorize]
    public class EditModel : PageModel
    {     
        [BindProperty]
        public Mythology editableMythology { get; set; }

        private NewStartDbContext database;
        public EditModel(NewStartDbContext database)
        {
            this.database = database;
        }

        public async Task OnGetAsync(int id)
        {
            editableMythology = await database.Mythologies.SingleAsync(m => m.ID == id);  
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
               return Page();
            }

            var dbMythology = await database.Mythologies.SingleAsync(m => m.ID == id);

            dbMythology.Name = editableMythology.Name;
            dbMythology.OrginCountry = editableMythology.OrginCountry;
            dbMythology.Date = editableMythology.Date;

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}