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


namespace NewStart.Pages.UnitUserReviews
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

        public List<string> UnitList { get; set; }

        public string UnitName { get; set; }

        [BindProperty]
        public UnitUserReview EditReviews { get; set; }

        private NewStartDbContext database;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EditReviews = await database.UnitUserReviews.Include(uur => uur.Unit).FirstOrDefaultAsync(u => u.ID == id);

            UnitList = await database.Units.Select(m => m.Name).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (EditReviews.Review == null || EditReviews.Review == "")
            {
                UnitList = await database.Units.Select(m => m.Name).ToListAsync();
                return Page();
            }

            var dbEMUR = await database.UnitUserReviews.Include(uur => uur.Unit).SingleAsync(m => m.ID == id);

            dbEMUR.Review = EditReviews.Review;
            var unit = database.Units.Where(EMUR => EMUR.Name == EditReviews.Unit.Name).First();
            dbEMUR.UnitID = unit.ID;
            IdentityUser user = await userManager.GetUserAsync(User);
            dbEMUR.UserID = user.Id;

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
