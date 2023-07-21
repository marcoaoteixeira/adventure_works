namespace Nameless.AdventureWorks {

    public sealed class Prevent {
        #region Private Static Read-Only Fields

        private static readonly Prevent _instance = new();

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets the singleton instance of <see cref="Prevent" />.
        /// </summary>
        public static Prevent Against => _instance;

        #endregion

        #region Static Constructors

        // Explicit static constructor to tell the C# compiler
        // not to mark type as beforefieldinit
        static Prevent() { }

        #endregion

        #region Private Constructors

        private Prevent() { }

        #endregion
    }
}