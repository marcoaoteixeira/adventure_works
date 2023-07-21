namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureRouting(IServiceCollection services) {
            services.AddRouting();
        }

        private static void UseRouting(IApplicationBuilder appBuilder) {
            appBuilder.UseRouting();
        }

        #endregion
    }
}