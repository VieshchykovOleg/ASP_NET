using Microsoft.AspNetCore.Identity;
using SurveyPortal.Data.Models;

namespace SurveyPortal.Models
{
    public static class IdentitySeedData
    {
        
        private const string adminUser = "admin@survey.com";
        private const string adminEmail = "admin@survey.com";
        private const string adminPassword = "Admin_P@ssw0rd123!";

        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                IdentityUser? user = await userManager.FindByNameAsync(adminUser);
                if (user == null)
                {
                    user = new IdentityUser(adminUser)
                    {
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    var createResult = await userManager.CreateAsync(user, adminPassword);

                    if (!createResult.Succeeded)
                    {
                        throw new InvalidOperationException($"Failed to create admin user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                    }
                }

                if (!await userManager.IsInRoleAsync(user, "Admin"))
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}