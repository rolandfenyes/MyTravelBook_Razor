using Microsoft.AspNetCore.Identity;
using MyTravelBook.Dal.Entities;
using MyTravelBook.Dal.SeedInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTravelBook.Dal.SeedService
{
    public class UserSeedService : IUserSeedService
    {
        private readonly UserManager<User> userManager;

        public UserSeedService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SeedUserAsync()
        {
            if (!(await userManager.GetUsersInRoleAsync(Roles.Roles.User)).Any())
            {
                var user = new User
                {
                    Email = "admin@mytravelbook.com",
                    Name = "Shishá Zoltán",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin"
                };

                var createResult = await userManager.CreateAsync(user, "#Administrator123");
                var addToRoleResult = await userManager.AddToRoleAsync(user, Roles.Roles.User);

                if (!createResult.Succeeded || !addToRoleResult.Succeeded)
                {
                    throw new ApplicationException("Administrator could not be created: " +
                        String.Join(",", createResult.Errors.Concat(addToRoleResult.Errors).Select(e => e.Description)));
                }
            }

        }
    }
}
