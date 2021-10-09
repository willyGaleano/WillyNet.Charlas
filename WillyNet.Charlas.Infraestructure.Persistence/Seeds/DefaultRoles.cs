using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Enums;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Infraestructure.Persistence.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
}
