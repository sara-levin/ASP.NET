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
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Mythology createdMythology { get; set; }

        private NewStartDbContext database;
        public CreateModel(NewStartDbContext database)
        {
            this.database = database;
        }

        public Task OnGetAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbMythology = new Mythology();

            dbMythology.Name = createdMythology.Name;
            dbMythology.OrginCountry = createdMythology.OrginCountry;
            dbMythology.Date = createdMythology.Date;
            database.Mythologies.Add(dbMythology);

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}