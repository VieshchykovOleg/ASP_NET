using Microsoft.AspNetCore.Identity;

namespace SurveyPortal.Models
{
    public static class IdentitySeedData
    {
        // Імена ролей
        private const string adminRole = "Admin";
        private const string userRole = "User";

        // Дані нашого адміна
        private const string adminUser = "Admin";
        private const string adminEmail = "admin@survey.com";
        private const string adminPassword = "AdminPassword123";

        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            // Отримуємо сервіси, необхідні для роботи з Identity
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

                // 1. Створення ролі "User"
                if (!await roleManager.RoleExistsAsync(userRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(userRole));
                }

                // 2. Створення ролі "Admin"
                if (!await roleManager.RoleExistsAsync(adminRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(adminRole));
                }

                // 3. Створення користувача-адміна
                IdentityUser? user = await userManager.FindByNameAsync(adminUser);
                if (user == null)
                {
                    user = new IdentityUser(adminUser)
                    {
                        Email = adminEmail,
                        EmailConfirmed = true // Одразу підтверджуємо
                    };
                    await userManager.CreateAsync(user, adminPassword);
                }

                // 4. Призначення ролі "Admin"
                if (!await userManager.IsInRoleAsync(user, adminRole))
                {
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }
        }
    }
}