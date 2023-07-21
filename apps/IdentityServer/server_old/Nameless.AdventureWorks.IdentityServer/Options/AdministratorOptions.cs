namespace Nameless.AdventureWorks.IdentityServer.Options {
    public sealed class AdministratorOptions {
        #region Public Static Read-Only Properties

        public static AdministratorOptions Default => new();

        #endregion

        #region Public Properties

        public Guid Id { get; set; } = Guid.Parse("45b48360-4ebf-4569-a5d4-c3ce4e10fbe9");
        public string FirstName { get; set; } = "Administrator";
        public string LastName { get; set; } = "Adventure Works";
        public string UserName { get; set; } = "administrator@adventureworks.com";
        public string Email { get; set; } = "administrator@adventureworks.com";
        public string Password { get; set; } = "123456AbC@";
        public string PhoneNumber { get; set; } = string.Empty;

        #endregion
    }
}
