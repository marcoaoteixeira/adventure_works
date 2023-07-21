namespace Nameless.AdventureWorks.IdentityServer.Api.v1.Models {
    public sealed class CreateUserInput {
        #region Public Properties

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }

        #endregion
    }
}
