using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using NewStart.Models;

namespace NewStart.Data
{
    public class NewStartDbContext : IdentityDbContext
    {
        public NewStartDbContext(DbContextOptions<NewStartDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mythology> Mythologies { get; set; }
        public DbSet<Unit> Units {get;set;}
        public DbSet<God> Gods { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MythologyUserReview> MythologyUserReviews { get; set; }
        public DbSet<GodUserReview> GodUserReviews { get; set; }
        public DbSet<UnitUserReview> UnitUserReviews { get; set; }
    }
}
