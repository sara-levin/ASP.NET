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
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public EditModel(NewStartDbContext database, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.database = database;
        }

        //public IList<Mythology> MythologyList { get; set; }

        public List<string> MythologyList { get; set; }

        public string MythologyName { get; set; }

        [BindProperty]
        public MythologyUserReview EditMythologyUserReviews { get; set; }

        private NewStartDbContext database;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EditMythologyUserReviews = await database.MythologyUserReviews.Include(mur => mur.Mythology).FirstOrDefaultAsync(u => u.ID == id);

            MythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (EditMythologyUserReviews.Review == null || EditMythologyUserReviews.Review == "")
            {
                MythologyList = await database.Mythologies.Select(m => m.Name).ToListAsync();
                return Page();
            }
           
            var dbEMUR = await database.MythologyUserReviews.Include(EMUR => EMUR.Mythology).SingleAsync(m => m.ID == id);

            dbEMUR.Review = EditMythologyUserReviews.Review;
            var myth = database.Mythologies.Where(EMUR => EMUR.Name == EditMythologyUserReviews.Mythology.Name).First();
            dbEMUR.MythologyID = myth.ID;
            IdentityUser user = await userManager.GetUserAsync(User);
            dbEMUR.UserID = user.Id;

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}
