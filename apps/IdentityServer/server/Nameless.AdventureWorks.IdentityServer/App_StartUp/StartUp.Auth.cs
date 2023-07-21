using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Nameless.AdventureWorks.Middlewares;
using Nameless.AdventureWorks.Options;

namespace Nameless.AdventureWorks.IdentityServer {
    public partial class StartUp {
        #region Private Static Methods

        private static void ConfigureAuth(IServiceCollection services, IConfiguration config) {
            var jwtSectionName = nameof(JwtOptions).TrimEnd("Options");
            var options = config
               .GetSection(jwtSectionName)
               .Get<JwtOptions>() ?? JwtOptions.Default;

            services
                .AddAuthentication(opts => {
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opts => {
                    var secret = Defaults.Encodings.UTF8.GetBytes(options.Secret);
                    opts.RequireHttpsMetadata = options.RequireHttpsMetadata;
                    opts.SaveToken = true;
                    opts.TokenValidationParameters = options.GetTokenValidationParameters();
                    opts.Events = new JwtBearerEvents {
                        OnAuthenticationFailed = ctx => {
                            if (ctx.Exception is SecurityTokenExpiredException) {
                                ctx.Response.Headers.Add("Is-Token-Expired", bool.TrueString);
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        private static void UseAuth(IApplicationBuilder app) {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseJwtAuthorization();
        }

        #endregion
    }
}