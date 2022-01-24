using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewStart.Models;

namespace NewStart.Pages.UnitUserReviews
{
    [Authorize]

    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly Data.NewStartDbContext database;

        public IndexModel(Data.NewStartDbContext context, UserManager<IdentityUser> userManager)
        {
            this.database = context;
            this.userManager = userManager;
        }

        public IList<UnitUserReview> UnitUserReviews { get; set; }

        public async Task OnGet()
        {
            IdentityUser user = await userManager.GetUserAsync(User);
            UnitUserReviews = await database.UnitUserReviews.Include(uur => uur.Unit).Where(r => r.UserID == user.Id).ToListAsync();
        }
    }
}