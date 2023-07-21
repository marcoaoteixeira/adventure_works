using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nameless.AdventureWorks.IdentityServer.Api.v1.Models {
    public sealed class AuthenticationInput {
        #region Public Properties

        [Required]
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; } = string.Empty;
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        #endregion
    }
}
