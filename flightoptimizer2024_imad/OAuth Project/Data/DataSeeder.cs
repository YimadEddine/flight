using Microsoft.AspNetCore.Identity;
using OAuth_Project.Models;
using System.Diagnostics;

namespace OAuth_Project.Data
{
    public static class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminUserName = configuration["AdminUserName"];
            string adminEmail = configuration["AdminEmail"];
            string adminPassword = configuration["AdminPassword"];
            string FirstName = "Imad Eddine";
            string LastName = "Youssef";
            string PhoneNumber = "0620296894";
            Debug.WriteLine("admin pass", adminPassword);
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var adminUser = new AppUser { UserName = adminUserName, Email = "admin@gmail.com", FirstName = FirstName, LastName = LastName, PhoneNumber = PhoneNumber };
                var result = await userManager.CreateAsync(adminUser, "Sephe9852@");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
