using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Infrastructure.Data
{
    internal class StoreIdentityDbInitializer(StoreIdentityDbContext dbContext,
        ILogger<StoreIdentityDbInitializer> logger,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager) : IDbInitializer
    {
        public async Task MigrateAsync(CancellationToken token = default)
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(token);
            if (!pendingMigrations.Any()) return;

            logger.LogInformation($"Applying {pendingMigrations.Count()} Migrations.");
            await dbContext.Database.MigrateAsync(token);
        }

        public async Task SeedAsync(CancellationToken token = default)
        {
            try
            {
                if (!await roleManager.Roles.AnyAsync(token))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!await userManager.Users.AnyAsync(token))
                {
                    var admin = new ApplicationUser
                    {
                        UserName = "Admin",
                        DisplayName = "Admin",
                        Email = "admin@gmail.com",
                        PhoneNumber = "1234567890"
                    };
                    var result = await userManager.CreateAsync(admin, "P@ssw0rd");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "Admin");
                    }
                    else
                    {
                        logger.LogWarning($"Couldn't Seed Admin User: {result.Errors}");
                    }
                    var superAdmin = new ApplicationUser
                    {
                        UserName = "SuperAdmin",
                        DisplayName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "1234567890"
                    };
                    result = await userManager.CreateAsync(superAdmin, "P@ssw0rd2");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                    }
                    else
                    {
                        logger.LogWarning($"Couldn't Seed Super Admin User: {result.Errors}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Seeding Failed: {ex.Message}");
            }
        }
    }
}
