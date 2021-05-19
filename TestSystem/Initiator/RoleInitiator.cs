using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DAL.Entities;

namespace TestSystem.Initiator
{
    public class RoleInitiator
    {         
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (await roleManager.FindByNameAsync("Administrator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Administrator"));
            }
            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("User"));
            }
        }
    }
}
