using FluentValidation;

namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureFluentValidation(IServiceCollection services) {
            services.AddValidatorsFromAssemblies(
                assemblies: new[] { typeof(StartUp).Assembly }
            );
        }

        #endregion
    }
}