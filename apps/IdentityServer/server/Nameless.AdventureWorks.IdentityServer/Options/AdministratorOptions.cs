namespace Nameless.AdventureWorks.IdentityServer.Options {
    public sealed class AdministratorOptions {
        #region Public Static Read-Only Properties

        public static AdministratorOptions Default => new();

        #endregion

        #region Public Properties

        public Guid Id { get; set; } = Guid.Parse("030cf713-ce4e-45ed-bdfa-4373cebb9666");
        public string FirstName { get; set; } = "Administrator";
        public string LastName { get; set; } = "Adventure Works";
        public string UserName { get; set; } = "administrator@adventureworks.com";
        public string Email { get; set; } = "administrator@adventureworks.com";
        public string Password { get; set; } = "123456AbC@";
        public string PhoneNumber { get; set; } = string.Empty;

        #endregion
    }
}
