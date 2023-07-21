namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Private Methods

        private void UseErrorHandling(IApplicationBuilder appBuilder, IWebHostEnvironment webHostEnvironment) {
            if (webHostEnvironment.IsDevelopment()) {
                appBuilder.UseDeveloperExceptionPage();
            }
        }

        #endregion
    }
}