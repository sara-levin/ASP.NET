using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewStart.Data;
using NewStart.Models;

namespace NewStart.Pages.MythologyUserReviews
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public DeleteModel(NewStartDbContext database, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.database = database;
        }

        public IList<Mythology> Mythologies { get; set; }

        public MythologyUserReview MythologyUserReviews { get; set; }

        public List<string> MythologyList { get; set; }

        public string MythologyName { get; set; }

        private NewStartDbContext database;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MythologyUserReviews = await database.MythologyUserReviews.Include(mur => mur.Mythology).FirstOrDefaultAsync(u => u.ID == id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var dbMythology = await database.MythologyUserReviews.FindAsync(id);
            database.MythologyUserReviews.Remove(dbMythology);

            await database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}

