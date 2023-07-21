using System.ComponentModel.DataAnnotations;

namespace Nameless.AdventureWorks.IdentityServer.Entities {
    public sealed class UserRefreshToken : EntityBase<Guid> {
        #region Public Properties

        [Required]
        [MaxLength(4096)]
        public string? Token { get; set; }
        public DateTime ExpiresIn { get; set; }
        public DateTime CreatedIn { get; set; }
        [MaxLength(64)]
        public string? CreatedByIp { get; set; }
        public DateTime? RevokedIn { get; set; }
        [MaxLength(64)]
        public string? RevokedByIp { get; set; }
        [MaxLength(4096)]
        public Guid? ReplacedByToken { get; set; }
        [MaxLength(1024)]
        public string? RevokeReason { get; set; }
        public Guid UserId { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves if user refresh token was revoked. Should not be used on EntityFramework queries.
        /// </summary>
        public bool IsRevoked() => RevokedIn != default;

        /// <summary>
        /// Retrieves if user refresh token was expired. Should not be used on EntityFramework queries.
        /// </summary>
        public bool IsExpired(DateTime? date = default) => (date != null ? date : DateTime.UtcNow) >= ExpiresIn;

        /// <summary>
        /// Retrieves if user refresh token still active. Should not be used on EntityFramework queries.
        /// </summary>
        public bool IsActive(DateTime? date = default) => !IsRevoked() && !IsExpired(date);

        #endregion
    }
}
