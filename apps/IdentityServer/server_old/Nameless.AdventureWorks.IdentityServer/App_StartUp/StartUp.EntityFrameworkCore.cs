using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sys_Environment = System.Environment;

namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureEntityFrameworkCore(IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(opts => {
                // Get the environment variable telling that we're running on Docker
                var isDocker = Sys_Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");
                var connectionStringName = isDocker.IsTrueString()
                    ? $"{nameof(ApplicationDbContext)}_Docker"
                    : $"{nameof(ApplicationDbContext)}";

                var connectionString = configuration.GetConnectionString(connectionStringName);
                opts.UseSqlServer(connectionString);
            });
        }

        private static void UseEntityFrameworkCore(IApplicationBuilder appBuilder) {
            using var scope = appBuilder.ApplicationServices.CreateScope();

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
