using Nameless.AdventureWorks.IdentityServer.Options;
using Nameless.AdventureWorks.Options;
using Nameless.AspNetCore;
using Nameless.AspNetCore.Options;

namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureOptions(IServiceCollection services, IConfiguration config) {
            services.AddOptions();

            services
                .PushOptions<SwaggerPageOptions>(config)
                .PushOptions<AdministratorOptions>(config)
                .PushOptions<JwtOptions>(config);
        }

        #endregion
    }
}