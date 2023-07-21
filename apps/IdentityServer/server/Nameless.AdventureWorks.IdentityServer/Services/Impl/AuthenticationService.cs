using Microsoft.AspNetCore.Identity;
using Nameless.AdventureWorks.IdentityServer.Entities;
using Nameless.AdventureWorks.Services;

namespace Nameless.AdventureWorks.IdentityServer.Services.Impl {
    public sealed class AuthenticationService : IAuthenticationService {
        #region Private Read-Only Fields

        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;

        #endregion

        #region Public Constructors

        public AuthenticationService(SignInManager<User> signInManager, IJwtService jwtService) {
            _signInManager = Prevent.Against.Null(signInManager, nameof(signInManager));
            _jwtService = Prevent.Against.Null(jwtService, nameof(jwtService));
        }

        #endregion

        #region IAuthenticationService Members

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, CancellationToken cancellationToken = default) {
            var result = await _signInManager.PasswordSignInAsync(
                userName: request.Identifier,
                password: request.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (!result.Succeeded) {
                return AuthenticationResponse.Failure("Invalid username or password.");
            }

            var user = await _signInManager.UserManager.FindByNameAsync(request.Identifier);
            if (user == null) {
                return AuthenticationResponse.Failure("Invalid username or password.");
            }

            var jwt = _jwtService.Generate(
                user.Id.ToString(),
                user.UserName ?? string.Empty,
                user.Email ?? string.Empty
            );

            return AuthenticationResponse.Success(
                user.Id,
                user.FirstName ?? string.Empty,
                user.LastName ?? string.Empty,
                user.UserName ?? string.Empty,
                user.Email ?? string.Empty,
                jwt
            );
        }

        #endregion
    }
}
