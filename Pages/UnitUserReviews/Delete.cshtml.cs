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
    public class DeleteModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public DeleteModel(NewStartDbContext database, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.database = database;
        }

        public IList<Unit> Units { get; set; }

        public UnitUserReview UnitUserReviews { get; set; }

        public List<string> UnitList { get; set; }

        private NewStartDbContext database;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            UnitUserReviews = await database.UnitUserReviews.Include(uur => uur.Unit).FirstOrDefaultAsync(u => u.ID == id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var dbUnit = await database.UnitUserReviews.FindAsync(id);
            database.UnitUserReviews.Remove(dbUnit);

            await database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
