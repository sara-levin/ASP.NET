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
        public class CreateModel : PageModel
        {
            private readonly UserManager<IdentityUser> userManager;

            public CreateModel(NewStartDbContext database, UserManager<IdentityUser> userManager)
            {
                this.userManager = userManager;
                this.database = database;
            }

            public IList<God> Gods { get; set; }

            //public IList<GodUserReview> Reviews { get; set; }

            public List<string> GodList { get; set; }

            //public string GodName { get; set; }

            [BindProperty]
            public GodUserReview CreatedReview { get; set; }

            private NewStartDbContext database;

            public async Task OnGetAsync()
            {
                Gods = await database.Gods.ToListAsync();
                GodList = await database.Gods.Select(m => m.Name).ToListAsync();

            }

            public async Task<IActionResult> OnPostAsync()
            {
                if (CreatedReview.Review == null || CreatedReview.Review == "")
                {
                    GodList = await database.Mythologies.Select(m => m.Name).ToListAsync();
                    return Page();
                }

                var dbUnit = new GodUserReview();
                dbUnit.Review = CreatedReview.Review;
                IdentityUser user = await userManager.GetUserAsync(User);
                CreatedReview.UserID = user.Id;
                dbUnit.UserID = CreatedReview.UserID;
                var god = database.Gods.Where(g => g.Name == CreatedReview.God.Name).First();
                dbUnit.GodID = god.ID;

                database.GodUserReviews.Add(dbUnit);
                await database.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

        }

}
