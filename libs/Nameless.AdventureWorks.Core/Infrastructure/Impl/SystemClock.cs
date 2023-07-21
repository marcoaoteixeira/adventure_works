namespace Nameless.AdventureWorks.Infrastructure.Impl {
    public sealed class SystemClock : IClock {
        #region Private Static Read-Only Fields

        private static readonly SystemClock _instance = new();

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets the unique instance of <see cref="SystemClock" />.
        /// </summary>
        public static IClock Instance => _instance;

        #endregion

        #region Static Constructors

        // Explicit static constructor to tell the C# compiler
        // not to mark type as beforefieldinit
        static SystemClock() { }

        #endregion

        #region Private Constructors

        private SystemClock() { }

        #endregion

        #region IClock Members

        public DateTime UtcNow => DateTime.UtcNow;
        public DateTimeOffset OffsetUtcNow {
            get {
                return new(new DateTime(
                    ticks: DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond,
                    kind: DateTimeKind.Utc
                ));
            }
        }

        #endregion
    }
}
