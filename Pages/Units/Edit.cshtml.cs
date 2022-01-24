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
    public class EditModel : PageModel
    {
        public IList<Unit> Units { get; set; }
        public IList<Mythology> Mythologies { get; set; }
        public List<string> mythologyList { get; set; }
              
        [BindProperty]
        public Unit editableUnit { get; set; }
       
        private NewStartDbContext database;
        public EditModel(NewStartDbContext database)
        {
            this.database = database;
        }

        public async Task OnGetAsync(int id)
        {
            editableUnit = await database.Units.Include(u => u.Mythology).SingleAsync(u => u.ID == id);
            Mythologies = await database.Mythologies.ToListAsync();            
            mythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var dbUnit = await database.Units.Include(u => u.Mythology).SingleAsync(u => u.ID == id);
            dbUnit.Name = editableUnit.Name;
            dbUnit.Class = editableUnit.Class;
            dbUnit.Hitpoints = editableUnit.Hitpoints;
            dbUnit.Attack = editableUnit.Attack;
            dbUnit.Cost = editableUnit.Cost;

            var mythology = database.Mythologies.Where(myth => myth.Name == editableUnit.Mythology.Name).First();
            dbUnit.MythologyID = mythology.ID;

            if(!ModelState.IsValid)
            {
                 mythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();
                return Page();                   
            }
            

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        
    } 
}

