using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Data
{
    public class StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Address> Addresses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(x => x.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
