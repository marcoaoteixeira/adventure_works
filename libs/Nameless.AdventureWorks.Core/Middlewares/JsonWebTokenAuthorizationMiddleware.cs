using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Nameless.AdventureWorks.Services;

namespace Nameless.AdventureWorks.Middlewares {
    public sealed class JsonWebTokenAuthorizationMiddleware {
        #region Private Read-Only Fields

        private readonly RequestDelegate _next;
        private readonly IJWTService _jsonWebTokenService;

        #endregion

        #region Public Constructors

        public JsonWebTokenAuthorizationMiddleware(RequestDelegate next, IJWTService jsonWebTokenService) {
            _next = Prevent.Against.Null(next, nameof(next));
            _jsonWebTokenService = Prevent.Against.Null(jsonWebTokenService, nameof(jsonWebTokenService));
        }

        #endregion

        #region Public Methods

        public async Task InvokeAsync(HttpContext context) {
            var key = HttpRequestHeader.Authorization.ToString();
            var token = context.Request.Headers[key].FirstOrDefault();

            if (token != null) {
                var values = token.Split(Constants.Separators.SPACE);
                if (_jsonWebTokenService.Validate(values[1], out var principal)) {
                    context.User = principal;
                }
            }

            await _next(context);
        }

        #endregion
    }

    public static class JsonWebTokenAuthorizationMiddlewareExtension {
        #region Public Static Methods

        public static IApplicationBuilder UseJsonWebTokenAuthorization(this IApplicationBuilder self)
            => self.UseMiddleware<JsonWebTokenAuthorizationMiddleware>();

        #endregion
    }
}
