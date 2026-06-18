using BrightPath.Controllers;
using BrightPath.Models;
using Microsoft.AspNetCore.Identity;

namespace BrightPath.Roles
{
    public class AdminSeeder
    {
        public static async Task adminseeterasync(IServiceProvider serviceProvider)
        {
            var usermanager = serviceProvider.GetRequiredService<UserManager<applicationuser>>();
            var adminEmail="yostromani42@gmail.com";
            var email=await usermanager.FindByEmailAsync(adminEmail);
            if (email == null)
            {
               var newadmin = new applicationuser
               {
                    UserName = adminEmail,
                    Email = adminEmail,
                    name= adminEmail,
                };
                await usermanager.CreateAsync(newadmin, "Rtheysma9*");
                await usermanager.AddToRoleAsync(newadmin, "Admin");

            }
        }
    }
}
