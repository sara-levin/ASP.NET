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
    public class CreateModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public CreateModel(NewStartDbContext database, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.database = database;
        }

        public IList<Unit> Units { get; set; }

        //public IList<GodUserReview> Reviews { get; set; }

        public List<string> UnitList { get; set; }

        //public string GodName { get; set; }

        [BindProperty]
        public UnitUserReview CreatedReview { get; set; }

        private NewStartDbContext database;

        public async Task OnGetAsync()
        {
            Units = await database.Units.ToListAsync();
            UnitList = await database.Units.Select(m => m.Name).ToListAsync();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CreatedReview.Review == null || CreatedReview.Review == "")
            {
                UnitList = await database.Units.Select(m => m.Name).ToListAsync();
                return Page();
            }

            var dbUnit = new UnitUserReview();
            dbUnit.Review = CreatedReview.Review;
            IdentityUser user = await userManager.GetUserAsync(User);
            CreatedReview.UserID = user.Id;
            dbUnit.UserID = CreatedReview.UserID;
            var unit = database.Units.Where(u => u.Name == CreatedReview.Unit.Name).First();
            dbUnit.UnitID = unit.ID;

            database.UnitUserReviews.Add(dbUnit);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
