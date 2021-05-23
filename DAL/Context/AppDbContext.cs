using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Context
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Theme> Tests { get; set; }
        public DbSet<Theme> Questions { get; set; }
        public DbSet<Theme> Answers { get; set; }
        public DbSet<Result> Results { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
