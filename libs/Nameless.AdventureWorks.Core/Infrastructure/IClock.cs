namespace Nameless.AdventureWorks.Infrastructure {
    public interface IClock {
        #region Properties

        DateTime UtcNow { get; }
        DateTimeOffset OffsetUtcNow { get; }

        #endregion
    }
}
