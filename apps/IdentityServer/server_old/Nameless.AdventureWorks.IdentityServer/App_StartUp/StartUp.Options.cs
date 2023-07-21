using Nameless.AdventureWorks.Options;

namespace Nameless.AdventureWorks.IdentityServer
{
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureOptions(IServiceCollection services, IConfiguration configuration) {
            services.AddOptions();

            services
                .FetchConfiguration<AdministratorOptions>(configuration)
                .FetchConfiguration<JwtOptions>(configuration)
                .FetchConfiguration<SwaggerPageOptions>(configuration);
        }

        #endregion
    }
}