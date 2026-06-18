using Microsoft.AspNetCore.Identity;

namespace BrightPath.Roles
{
    public class RoleSeeder
    {
        public static async Task seedroleasync(IServiceProvider serviceProvider)
        {
            var rolemanager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Student", "Admin" };
            foreach (var role in roles)
            {
                if (!await rolemanager.RoleExistsAsync(role))
                {
                    await rolemanager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
