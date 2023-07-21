using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Public Properties

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }

        #endregion

        #region Public Constructors

        public StartUp(IConfiguration configuration, IHostEnvironment hostEnvironment) {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        #endregion

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection serviceCollection) {
            ConfigureOptions(serviceCollection, Configuration);

            ConfigureEntityFrameworkCore(serviceCollection, Configuration);

            ConfigureIdentity(serviceCollection);

            ConfigureAutoMapper(serviceCollection);

            ConfigureFluentValidation(serviceCollection);

            ConfigureCors(serviceCollection);

            ConfigureAuth(serviceCollection, Configuration);

            ConfigureRouting(serviceCollection);

            ConfigureEndpoints(serviceCollection);

            ConfigureSwagger(serviceCollection);

            ConfigureVersioning(serviceCollection);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment, IHostApplicationLifetime hostApplicationLifetime, IApiVersionDescriptionProvider apiVersionDescriptionProvider) {
            UseSwagger(applicationBuilder, webHostEnvironment, apiVersionDescriptionProvider);

            UseCors(applicationBuilder);

            UseEntityFrameworkCore(applicationBuilder);

            UseRouting(applicationBuilder);

            UseAuth(applicationBuilder);

            UseEndpoints(applicationBuilder);

            UseHttpSecurity(applicationBuilder, webHostEnvironment);

            UseErrorHandling(applicationBuilder, webHostEnvironment);

            var container = applicationBuilder.ApplicationServices.GetAutofacRoot();

            // Tear down the composition root and free all resources.
            hostApplicationLifetime.ApplicationStopped.Register(container.Dispose);
        }

        #endregion
    }
}