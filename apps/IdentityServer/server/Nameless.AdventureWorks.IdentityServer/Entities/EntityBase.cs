using System.ComponentModel.DataAnnotations;

namespace Nameless.AdventureWorks.IdentityServer.Entities {
    public abstract class EntityBase<TKey> where TKey : struct, IEquatable<TKey> {
        #region Public Virtual Properties

        [Key]
        public virtual TKey Id { get; set; }

        #endregion
    }
}
