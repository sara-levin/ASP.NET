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
    public class CreateModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public CreateModel(NewStartDbContext database, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.database = database;
        }

        public IList<Mythology> Mythologies { get; set; }

        public IList<MythologyUserReview> Reviews { get; set; }

        public List<string> MythologyList { get; set; }

        public string MythologyName { get; set; }

        [BindProperty]
        public MythologyUserReview CreatedReview { get; set; }

        private NewStartDbContext database;

        public async Task OnGetAsync()
        {
            Mythologies = await database.Mythologies.ToListAsync();
            MythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CreatedReview.Review == null || CreatedReview.Review == "")
            {
                MythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();
                return Page();
            }

            var dbUnit = new MythologyUserReview();
            dbUnit.Review = CreatedReview.Review;
            IdentityUser user = await userManager.GetUserAsync(User);
            CreatedReview.UserID = user.Id;
            dbUnit.UserID = CreatedReview.UserID;
            var mythology = database.Mythologies.Where(myth => myth.Name == CreatedReview.Mythology.Name).First();
            dbUnit.MythologyID = mythology.ID;

            database.MythologyUserReviews.Add(dbUnit);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}
