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

namespace NewStart.Pages.Units
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public IList<Unit> Units { get; set; }

        public IList<Mythology> Mythologies { get; set; }

        public List<string> mythologyList { get; set; }

        [BindProperty]
        public Unit createdUnit { get; set; }

        private NewStartDbContext database;
        public CreateModel(NewStartDbContext database)
        {
            this.database = database;
        }

        public async Task OnGetAsync()
        {           
            Mythologies = await database.Mythologies.ToListAsync();
            mythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dbUnit = new Unit();
            dbUnit.Name = createdUnit.Name;
            dbUnit.Class = createdUnit.Class;
            dbUnit.Hitpoints = createdUnit.Hitpoints;
            dbUnit.Attack = createdUnit.Attack;
            dbUnit.Cost = createdUnit.Cost;
            var mythology = database.Mythologies.Where(myth => myth.Name == createdUnit.Mythology.Name).First();
            dbUnit.MythologyID = mythology.ID;

            if(!ModelState.IsValid)
            {
                mythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();
               return Page();                   
            }


            database.Units.Add(dbUnit);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}

