using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Enums;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Infraestructure.Persistence.Seeds
{
    public class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default Admin User
            var defaultUser = new UserApp
            {
                UserName = "Jun",
                Email = "sofia@mail.com",
                FirstName = "Sofía",
                LastName = "Parente",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Holamundo123*");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }
    }
}
