using Book_Store.Models;
using Microsoft.AspNetCore.Identity;

namespace Book_Store.Repository
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<AppCustomer> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleNames = new[] { "Admin", "Customer" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Tạo Role nếu nó chưa tồn tại
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Tạo user admin để test
            AppCustomer? user = await userManager.FindByEmailAsync("admin@gmail.com");

            if (user == null)
            {
                user = new AppCustomer()
                {
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                };

                //  Tạo Password
                var passwordHasher = new PasswordHasher<AppCustomer>();
                user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    //await userManager.CreateAsync(user, "Admin");
                    await userManager.AddToRoleAsync(user, "Admin");
                }

            }
        }
    }

}
