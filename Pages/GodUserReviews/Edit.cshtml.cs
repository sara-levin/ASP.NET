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
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public EditModel(NewStartDbContext database, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.database = database;
        }

        public List<string> GodList { get; set; }

        public string GodName { get; set; }

        [BindProperty]
        public GodUserReview EditReviews { get; set; }

        private NewStartDbContext database;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EditReviews = await database.GodUserReviews.Include(mur => mur.God).FirstOrDefaultAsync(u => u.ID == id);

            GodList = await database.Gods.Select(m => m.Name).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (EditReviews.Review == null || EditReviews.Review == "")
            {
                GodList = await database.Gods.Select(m => m.Name).ToListAsync();
                return Page();
            }

            var dbEMUR = await database.GodUserReviews.Include(gur => gur.God).SingleAsync(m => m.ID == id);

            dbEMUR.Review = EditReviews.Review;
            var god = database.Gods.Where(EMUR => EMUR.Name == EditReviews.God.Name).First();
            dbEMUR.GodID = god.ID;
            IdentityUser user = await userManager.GetUserAsync(User);
            dbEMUR.UserID = user.Id;

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
