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
    public class EditModel : PageModel
    {
        public IList<God> Gods { get; set; }
        public IList<Mythology> Mythologies { get; set; }
        public List<string> mythologyList { get; set; }

        [BindProperty]
        public God editableGod { get; set; }

        private NewStartDbContext database;
        public EditModel(NewStartDbContext database)
        {
            this.database = database;
        }

        public async Task OnGetAsync(int id)
        {
            editableGod = await database.Gods.SingleAsync(m => m.ID == id);
            Mythologies = await database.Mythologies.ToListAsync();
            mythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();

        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                mythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();
                return Page();
            }

            var dbGod = await database.Gods.SingleAsync(m => m.ID == id);

            dbGod.Name = editableGod.Name;
            dbGod.Trivia = editableGod.Trivia;
            dbGod.Age = editableGod.Age;
            dbGod.Focus = editableGod.Focus;

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}