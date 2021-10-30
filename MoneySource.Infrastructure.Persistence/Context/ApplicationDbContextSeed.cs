using Microsoft.AspNetCore.Identity;
using MoneySource.Core.Application.Authentication;
using MoneySource.Core.Domain.Enums;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Infrastructure.Persistence.Context
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager)
        {
            var roles = Enum.GetNames(typeof(BaseRole));
            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            foreach (var role in roleManager.Roles.ToList())
            {
                if (!roles.Contains(role.Name))
                {
                    await roleManager.DeleteAsync(role);
                }
            }

            if (!userManager.Users.Any())
            {
                var d = DemoUsers.DemoUsersList;
                foreach (var demoUsers in d)
                {
                    foreach (var user in demoUsers.Value)
                    {
                        var createdUser = await userManager.CreateAsync(user, DemoUsers.DefaultPassword);
                        if (createdUser.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, demoUsers.Key.ToString());
                        }
                    }

                }
            }
        }
    }
}
