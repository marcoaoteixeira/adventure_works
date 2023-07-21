using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Nameless.AdventureWorks.Extensions;

namespace Nameless.AdventureWorks.Services.Impl {
    public sealed class JwtService : IJWTService {
        #region Private Read-Only Fields

        private readonly JwtOptions _options;
        #endregion

        #region Public Properties

        private IClock? _clock;
        public IClock Clock {
            get { return _clock ??= SystemClock.Instance; }
            set { _clock = value; }
        }

        private ILogger? _logger;
        public ILogger Logger {
            get { return _logger ??= NullLogger.Instance; }
            set { _logger = value; }
        }

        #endregion

        #region Public Constructors

        public JwtService(JwtOptions? options = null) {
            _options = options ?? JwtOptions.Default;
        }

        #endregion

        #region IJsonWebTokenService Members

        public string Generate(string userId, string userName, string userEmail) {
            var issuer = _options.Issuer;
            var audience = _options.Audience;
            var now = Clock.UtcNow;
            var expires = now.AddSeconds(_options.AccessTokenTtl);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Issuer = issuer,
                Audience = audience,
                Claims = new Dictionary<string, object> {
                    // NOTE: Here JwtRegisteredClaimNames.Sub will be substituted by ClaimTypes.NameIdentifier
                    { JwtRegisteredClaimNames.Sub, userId },

                    { JwtRegisteredClaimNames.Iss, issuer },
                    { JwtRegisteredClaimNames.Exp, expires.ToString() },
                    { JwtRegisteredClaimNames.Iat, now.ToString() },
                    { JwtRegisteredClaimNames.Aud, audience },
                    { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() }
                },
                Expires = expires,
                SigningCredentials = new(
                    key: new SymmetricSecurityKey(_options.Secret.GetBytes()),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                ),
                Subject = new(new Claim[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Email, userEmail)
                })
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool Validate(string token, [NotNullWhen(true)] out ClaimsPrincipal? principal) {
            principal = null;

            try {
                principal = new JwtSecurityTokenHandler()
                    .ValidateToken(
                        token: token,
                        validationParameters: _options.GetTokenValidationParameters(),
                        validatedToken: out SecurityToken securityToken
                    );

                var validate = securityToken is JwtSecurityToken jwtSecurityToken &&
                    jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                if (!validate) { principal = null; }

                return validate;
            } catch (Exception ex) { Logger.Error(ex, ex.Message); }

            return false;
        }

        #endregion
    }
}
