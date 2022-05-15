using FoodStore.Infrastructure;
using FoodStore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStore.Data
{
    public static class SeedAdminData
    {
        private static Trie Trie;

        public static async Task Seed(UserManager<Customer> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRole(roleManager);
            await SeedUser(userManager);
        }

        private static async Task SeedUser(UserManager<Customer> userManager)
        {
            if(userManager.FindByNameAsync("admin").Result == null)
            {
                Customer customer = new Customer
                {
                    UserName = "admin",
                    Email = "admin@localhost.com",
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(customer, "SecurePa55word123!");

                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customer, "ADMINISTRATOR");
                }
            }
        }

        private static async Task SeedRole(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync("administrator").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "administrator"
                };

                await roleManager.CreateAsync(role);
            }
        }
    }
}
