using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Nameless.AdventureWorks {
    public static class PreventExtension {
        #region Public Static Methods

        [DebuggerStepThrough]
        public static T Null<T>(this Prevent _, [NotNull]T? input, string name, string? message = null) {
            return input ?? throw new ArgumentNullException(name, message ?? $"Input {name} is null.");
        }

        [DebuggerStepThrough]
        public static string NullOrWhiteSpace(this Prevent _, string input, string name, string? message = null) {
            if (input == null) {
                throw new ArgumentNullException(name, message ?? $"Input {name} is null.");
            }

            if (input.Trim().Length == 0) {
                throw new ArgumentException(message ?? $"Input {name} is empty or white space.", name);
            }

            return input;
        }

        [DebuggerStepThrough]
        public static T NullOrEmpty<T>(this Prevent _, T input, string name, string? message = null) where T : IEnumerable {
            if (input == null) {
                throw new ArgumentNullException(name, message ?? $"Input {name} is null.");
            }

            // Costs O(1)
            if (input is ICollection collection && collection.Count == 0) {
                throw new ArgumentException(message ?? $"Input {name} is empty.", name);
            }

            // Costs O(N)
            var enumerator = input.GetEnumerator();
            var canMoveNext = enumerator.MoveNext();
            if (enumerator is IDisposable disposable) {
                disposable.Dispose();
            }
            if (!canMoveNext) {
                throw new ArgumentException(message ?? $"Input {name} is empty.", name);
            }
            return input;
        }

        [DebuggerStepThrough]
        public static string NoMatchingPattern(this Prevent _, string input, string name, string pattern, string? message = null) {
            var match = Regex.Match(input, pattern);
            if (!match.Success || match.Value != input) {
                throw new ArgumentException(message ?? $"Input {name} does not match pattern {pattern}", name);
            }
            return input;
        }

        [DebuggerStepThrough]
        public static T Expression<T>(this Prevent _, Func<T, bool> expression, T input, string? message = null) {
            if (!expression(input)) {
                throw new ArgumentException(message ?? "Input did not pass expression test.");
            }
            return input;
        }

        #endregion
    }
}
