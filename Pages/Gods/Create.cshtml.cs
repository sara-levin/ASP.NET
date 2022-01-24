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

namespace NewStart.Pages.Gods
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public IList<God> Gods { get; set; }
        public IList<Mythology> Mythologies { get; set; }
        public List<string> mythologyList { get; set; }

        [BindProperty]
        public God createdGod { get; set; }

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
            if (!ModelState.IsValid)
            {
                mythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();
                return Page();
            }

            var dbGod = new God();
            dbGod.Name = createdGod.Name;
            dbGod.Trivia = createdGod.Trivia;
            dbGod.Age = createdGod.Age;
            dbGod.Focus = createdGod.Focus;
            var mythology = database.Mythologies.Where(myth => myth.Name == createdGod.Mythology.Name).First();
            dbGod.MythologyID = mythology.ID;

            database.Gods.Add(dbGod);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}
