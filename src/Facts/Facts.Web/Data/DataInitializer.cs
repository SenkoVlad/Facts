using Calabonga.Microservices.Core.Exceptions;
using Facts.Web.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Data
{
    public class DataInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var isExists = context!.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator
                && await databaseCreator.ExistsAsync();

            if (isExists)
                return;

            await context.Database.MigrateAsync();

            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var rolesManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            IdentityResult identityResult;
            var roles = AppData.Roles.ToArray();
            
            var roleStore = new RoleStore<IdentityRole>(context);

            if(userManager == null || rolesManager == null)
            {
                throw new MicroserviceArgumentNullException("UserManager or RoleManager isn't registered");
            }

            foreach (var role in roles)
            {
                if(!rolesManager.Roles.Any(item => item.Name == role))
                {
                    await rolesManager.CreateAsync(new IdentityRole(role) 
                    {
                        NormalizedName = role.ToUpper()
                    });
                }
            }
            const string username = "vlad.senko@mail.com";
            
            if(context.Users.Any(item => item.UserName == username))
            {
                return;
            }
            var user = new IdentityUser()
            {
                UserName = username,
                Email = username,
                NormalizedEmail = username.ToUpper(),
                NormalizedUserName = username.ToUpper(),
                PhoneNumber = "+375291112233",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "123456!#$@");

            identityResult = await userManager.CreateAsync(user);

            if(!identityResult.Succeeded)
            {
                var message = string.Join(", ", identityResult.Errors.Select(x => $"{x.Code}: {x.Description}"));
                throw new MicroserviceDatabaseException(message);
            }
            await userManager.AddToRolesAsync(user, roles);
            await context.SaveChangesAsync();
        }
    }
}
