namespace Nameless.AdventureWorks {
    public static class StringExtension {
        #region Public Static Methods

        public static byte[] GetBytes(this string self)
            => Defaults.Encodings.UTF8.GetBytes(self);

        public static bool IsTrueString(this string? self) {
            // we'll consider null as false.
            if (self == null) { return false; }

            // any numeric value less than 0 is false.
            if (double.TryParse(self, out var result)) {
                return result > 0d;
            }

            // self explanatory.
            return string.Equals(self, bool.TrueString, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Removes, from the end of the current string, the first occurence of
        /// <paramref name="values"/>.
        /// </summary>
        /// <param name="self">The current instance of <see cref="string"/>.</param>
        /// <param name="values">A collection of values to match.</param>
        /// <returns>The current <see cref="string"/> without the matching end, if exists.</returns>
        public static string TrimEnd(this string self, params string[] values) {
            foreach (var value in values) {
                if (self.EndsWith(value)) {
                    return self[..self.LastIndexOf(value)];
                }
            }
            return self;
        }

        #endregion
    }
}
