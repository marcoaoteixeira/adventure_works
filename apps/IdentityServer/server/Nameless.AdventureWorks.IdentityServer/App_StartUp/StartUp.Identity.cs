using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nameless.AdventureWorks.IdentityServer.Entities;
using Nameless.AdventureWorks.IdentityServer.Options;

namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureIdentity(IServiceCollection services) {
            services
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        private static void UseIdentity(IApplicationBuilder app) {
            using var scope = app.ApplicationServices.CreateScope();

            // Force migration.
            scope.ServiceProvider.GetService<ApplicationDbContext>()?.Database.Migrate();

            using var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
            if (userManager != null) {
                var administratorOptions = scope
                    .ServiceProvider
                    .GetService<AdministratorOptions>() ?? AdministratorOptions.Default;

                var hasAdminUser = userManager.Users.Any(_ => _.Email == administratorOptions.Email);
                if (!hasAdminUser) {
                    var adminUser = new User {
                        Id = administratorOptions.Id,
                        UserName = administratorOptions.UserName,
                        FirstName = administratorOptions.FirstName,
                        LastName = administratorOptions.LastName,
                        NormalizedUserName = administratorOptions.UserName.ToUpper(),
                        Email = administratorOptions.Email,
                        NormalizedEmail = administratorOptions.Email.ToUpper(),
                        EmailConfirmed = true,
                        PhoneNumber = administratorOptions.PhoneNumber,
                        PhoneNumberConfirmed = true
                    };

                    userManager.CreateAsync(adminUser).Wait();
                    userManager.AddPasswordAsync(adminUser, administratorOptions.Password).Wait();
                }
            }
        }

        #endregion
    }
}