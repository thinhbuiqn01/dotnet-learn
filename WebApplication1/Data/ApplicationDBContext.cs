
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
  public class ApplicationDBContext : IdentityDbContext<AppUser>
  {
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Portfolio> Portfolios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.CategoryId }));

      modelBuilder.Entity<Portfolio>()
        .HasOne(p => p.AppUser)
        .WithMany(p => p.Portfolios)
        .HasForeignKey(p => p.AppUserId);

      modelBuilder.Entity<Portfolio>()
        .HasOne(p => p.category)
        .WithMany(p => p.Portfolios)
        .HasForeignKey(p => p.CategoryId);

      List<IdentityRole> roles = new List<IdentityRole>
      {
        new IdentityRole
        {
          Name = "Admin",
          NormalizedName = "ADMIN",
        },
        new IdentityRole
        {
          Name = "User",
          NormalizedName = "USER",
        },
      };

      modelBuilder.Entity<IdentityRole>().HasData(roles);
    }


  }
}