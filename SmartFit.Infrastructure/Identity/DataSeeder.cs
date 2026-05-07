using Microsoft.AspNetCore.Identity;
using SmartFit.Domain.Entities;
using System.Threading.Tasks;

namespace SmartFit.Infrastructure.Identity
{
    public static class DataSeeder
    {
        public static async System.Threading.Tasks.Task SeedAdminAsync(
            UserManager<ApplicationUser> userManager)
        {
            // 🔥 Admin
            var adminEmail = "admin@smartfit.com";
            var adminPassword = "Admin123!";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // 🔥 Support
            var supportEmail = "support@smartfit.com";
            var supportPassword = "Support123!";

            var existingSupport = await userManager.FindByEmailAsync(supportEmail);

            if (existingSupport == null)
            {
                var support = new ApplicationUser
                {
                    UserName = supportEmail,
                    Email = supportEmail,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(support, supportPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(support, "Support");
                }
            }
        }
    }
}