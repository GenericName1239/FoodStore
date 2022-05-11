using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Data
{
    public static class SeedAdminData
    {
        public static void Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRole(roleManager);
            SeedUser(userManager);
        }

        private static void SeedUser(UserManager<IdentityUser> userManager)
        {
            if(userManager.FindByNameAsync("admin").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@localhost.com",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "SecurePa55word123!").Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "ADMINISTRATOR").Wait();
                }
            }
        }

        private static void SeedRole(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync("administrator").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "administrator"
                };

                roleManager.CreateAsync(role);
            }
        }
    }
}
