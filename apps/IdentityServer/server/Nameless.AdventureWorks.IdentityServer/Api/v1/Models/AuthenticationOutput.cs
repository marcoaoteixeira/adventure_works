using System.Text.Json.Serialization;

namespace Nameless.AdventureWorks.IdentityServer.Api.v1.Models {
    public sealed class AuthenticationOutput {
        #region Public Properties

        [JsonPropertyName("user_id")]
        public object UserId { get; set; } = null!;
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }
        [JsonPropertyName("username")]
        public string UserName { get; set; } = null!;
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
        [JsonPropertyName("jwt")]
        public string Jwt { get; set; } = null!;

        #endregion
    }
}
