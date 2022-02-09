using ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var admin = userManager.FindByNameAsync(AuthorizationConstants.DEFAULT_USER_NAME).GetAwaiter().GetResult();

            if (admin == null)
            {
                await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.DEFAULT_ROLE));

                var adminUser = new ApplicationUser
                {
                    UserName = AuthorizationConstants.DEFAULT_USER_NAME,
                    Email = AuthorizationConstants.DEFAULT_EMAIL,
                    EmailConfirmed = true,
                    Salt = Encoding.ASCII.GetBytes(AuthorizationConstants.DEFAULT_PASSWORD)
                };

                await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);

                var user = await userManager.FindByNameAsync(AuthorizationConstants.DEFAULT_USER_NAME);

                await userManager.AddToRoleAsync(user, AuthorizationConstants.DEFAULT_ROLE);
            }
        }
    }
}
