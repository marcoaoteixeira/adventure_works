using Microsoft.EntityFrameworkCore;
using Nameless.AdventureWorks.IdentityServer.Entities;
using Sys_Environment = System.Environment;

namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureEntityFrameworkCore(IServiceCollection services, IConfiguration config) {
            services.AddDbContext<ApplicationDbContext>(opts => {
                // Get the environment variable telling that we're running on Docker
                var isDocker = Sys_Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");
                var connectionStringName = isDocker.IsTrueString()
                    ? $"{nameof(ApplicationDbContext)}_Docker"
                    : $"{nameof(ApplicationDbContext)}";

                var connectionString = config.GetConnectionString(connectionStringName);
                opts.UseSqlServer(connectionString);
            });
        }

        #endregion
    }
}
