using Microsoft.IdentityModel.Tokens;
using Nameless.AdventureWorks.Options;

namespace Nameless.AdventureWorks.Extensions {
    public static class JsonWebTokenOptionsExtension {
        #region Public Static Methods

        public static TokenValidationParameters GetTokenValidationParameters(this JwtOptions self) {
            return new() {
                ValidateIssuer = self.ValidateIssuer,
                ValidateAudience = self.ValidateAudience,
                ValidateLifetime = self.ValidateLifetime,
                ValidateIssuerSigningKey = self.ValidateIssuerSigningKey,
                ValidIssuer = self.Issuer,
                ValidAudience = self.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(self.Secret.GetBytes()),
                ClockSkew = TimeSpan.FromSeconds(self.MaxClockSkew)
            };
        }

        #endregion
    }
}
