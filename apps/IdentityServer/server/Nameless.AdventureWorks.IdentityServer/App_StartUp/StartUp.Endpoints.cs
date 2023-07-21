namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureEndpoints(IServiceCollection services) {
            services.AddControllers();
        }

        private static void UseEndpoints(IApplicationBuilder app) {
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        #endregion
    }
}