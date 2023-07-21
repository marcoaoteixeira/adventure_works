namespace Nameless.AdventureWorks {
    public static class Constants {
        #region Public Static Inner Classes

        public static class Separators {
            #region Public Constants

            public const char SPACE = ' ';
            public const char DASH = '-';
            public const char COMMA = ',';
            public const char SEMICOLON = ';';
            public const char DOT = '.';

            #endregion
        }

        public static class RegexPatterns {
            #region Public Constants

            public const string PASSWORD = "^([a-zA-Z0-9@*#]{6,32})$";

            #endregion
        }

        #endregion
    }
}
