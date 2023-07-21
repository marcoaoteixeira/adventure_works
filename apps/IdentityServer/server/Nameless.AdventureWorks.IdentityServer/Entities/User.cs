using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Nameless.AdventureWorks.IdentityServer.Entities {
    public sealed class User : IdentityUser<Guid> {
        #region Public Virtual Properties

        [MaxLength(2048)]
        public string? AvatarUrl { get; set; }

        [MaxLength(2048)]
        public string? FirstName { get; set; }

        [MaxLength(2048)]
        public string? LastName { get; set; }

        #endregion
    }
}