using Autofac;
using Nameless.AdventureWorks.IdentityServer.Services.Impl;
using Nameless.AdventureWorks.Services;
using Nameless.AdventureWorks.Services.Impl;

namespace Nameless.AdventureWorks.IdentityServer
{
    public partial class StartUp {
        #region Public Methods

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder) {
            // register modules

            // register services
            builder
                .RegisterInstance(SystemClock.Instance)
                .As<IClock>();

            builder
                .RegisterType<JwtService>()
                .As<IJWTService>()
                .SingleInstance();

            builder
                .RegisterType<AuthenticationService>()
                .As<IAuthenticationService>()
                .InstancePerLifetimeScope();
        }


        #endregion
    }
}