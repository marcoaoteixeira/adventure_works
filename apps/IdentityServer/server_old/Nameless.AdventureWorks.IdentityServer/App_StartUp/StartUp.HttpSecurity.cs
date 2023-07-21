namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Private Static Methods

        private static void UseHttpSecurity(IApplicationBuilder appBuilder, IWebHostEnvironment webHostEnvironment) {
            if (!webHostEnvironment.IsDevelopment()) {
                // The default HSTS value is 30 days. You may want to change
                // this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                appBuilder.UseHsts();
            }

            appBuilder.UseHttpsRedirection();
        }

        #endregion
    }
}