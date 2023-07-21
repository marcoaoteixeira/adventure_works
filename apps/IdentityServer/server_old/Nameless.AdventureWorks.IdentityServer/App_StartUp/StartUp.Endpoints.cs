using Nameless.AdventureWorks.Filters;

namespace Nameless.AdventureWorks.IdentityServer
{
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureEndpoints(IServiceCollection services) {
            services.AddControllers(config => {
                config.Filters.Add<ValidateModelStateActionFilter>();
            });
        }

        private static void UseEndpoints(IApplicationBuilder appBuilder) {
            appBuilder.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }

        #endregion
    }
}