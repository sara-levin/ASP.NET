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

namespace NewStart.Pages.GodUserReviews
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

        public IList<God> Gods { get; set; }

        public GodUserReview GodUserReviews { get; set; }

        public List<string> GodList { get; set; }

        private NewStartDbContext database;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            GodUserReviews = await database.GodUserReviews.Include(gur => gur.God).FirstOrDefaultAsync(u => u.ID == id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var dbGod = await database.GodUserReviews.FindAsync(id);
            database.GodUserReviews.Remove(dbGod);

            await database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
