using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Nameless.AdventureWorks.Versioning;

namespace Nameless.AdventureWorks.IdentityServer
{
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureSwagger(IServiceCollection services) {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opts => {
                opts.OperationFilter<SwaggerDefaultValuesOperationFilter>();
                opts.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new() {
                    In = ParameterLocation.Header,
                    Description = "Enter JSON Web Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme.ToLower()
                });
                opts.AddSecurityRequirement(new() {
                    {
                        new() {
                            Reference = new() {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        private static void UseSwagger(IApplicationBuilder appBuilder, IHostEnvironment hostEnvironment, IApiVersionDescriptionProvider provider) {
            if (hostEnvironment.IsDevelopment()) {
                appBuilder.UseSwagger();
                appBuilder.UseSwaggerUI(opts => {
                    foreach (var description in provider.ApiVersionDescriptions) {
                        opts.SwaggerEndpoint(
                            url: $"/swagger/{description.GroupName}/swagger.json",
                            name: description.GroupName.ToUpperInvariant()
                        );
                    }
                });
            }
        }

        #endregion
    }
}