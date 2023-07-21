using System.Collections;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nameless.AdventureWorks.Models {
    public sealed record ClientErrorEntry {
        #region Public Properties

        [JsonPropertyName("code")]
        public string Code { get; }

        [JsonPropertyName("problems")]
        public string[] Problems { get; }

        #endregion

        #region Public Constructors

        public ClientErrorEntry(string code, string[] problems) {
            Code = Prevent.Against.Null(code, nameof(code));
            Problems = Prevent.Against.Null(problems, nameof(problems));
        }

        #endregion
    }

    public sealed class ClientErrorCollection : IEnumerable<ClientErrorEntry> {
        #region Private Read-Only Properties

        private Dictionary<string, ISet<string>> Entries { get; } = new(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Public Methods

        public void Push(string code, params string[] problems) {
            Prevent.Against.Null(code, nameof(code));
            Prevent.Against.Null(problems, nameof(problems));

            var entry = GetEntry(code);
            PushEntries(entry, problems);
        }

        #endregion

        #region Private Static Methods

        private static void PushEntries(ISet<string> entry, string[] problems) {
            foreach (var problem in problems) {
                if (string.IsNullOrWhiteSpace(problem)) {
                    continue;
                }
                entry.Add(problem);
            }
        }

        #endregion

        #region Private Methods

        private ISet<string> GetEntry(string code) {
            if (!Entries.ContainsKey(code)) {
                Entries.Add(code, new HashSet<string>());
            }
            return Entries[code];
        }

        private IEnumerable<ClientErrorEntry> Enumerate() {
            foreach (var item in Entries) {
                yield return new(item.Key, item.Value.ToArray());
            }
        }

        #endregion

        #region IEnumerable<ClientErrorEntry> Members

        IEnumerator<ClientErrorEntry> IEnumerable<ClientErrorEntry>.GetEnumerator()
            => Enumerate().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => Enumerate().GetEnumerator();

        #endregion
    }

    public static class ClientErrorCollectionExtension {
        #region Public Static Methods

        public static void Push(this ClientErrorCollection self, string code, Exception ex) {
            Prevent.Against.Null(ex, nameof(ex));

            var problems = ExtractProblemsFromException(ex);

            self.Push(code, problems);
        }

        public static ClientErrorCollection ToClientError(this ModelStateDictionary self) {
            var result = new ClientErrorCollection();

            foreach (var kvp in self) {
                var code = kvp.Key;
                var problems = kvp.Value.Errors.SelectMany(ExtractProblemsFromModelError);

                result.Push(code, problems.ToArray());
            }

            return result;
        }

        public static ClientErrorCollection ToClientError(this ValidationResult self) {
            var result = new ClientErrorCollection();

            foreach (var failure in self.Errors) {
                result.Push(failure.ErrorCode, failure.ErrorMessage);
            }

            return result;
        }

        public static ClientErrorCollection ToClientError(this IdentityResult self) {
            var result = new ClientErrorCollection();

            foreach (var error in self.Errors) {
                result.Push(error.Code, error.Description);
            }

            return result;
        }

        #endregion

        #region Private Static Methods

        private static string[] ExtractProblemsFromModelError(ModelError modelError) {
            var problems = new List<string> {
                modelError.ErrorMessage
            };

            if (modelError.Exception != null) {
                var lines = ExtractProblemsFromException(modelError.Exception);

                problems.AddRange(lines);
            }

            return problems.ToArray();
        }

        private static string[] ExtractProblemsFromException(Exception ex) {
            var stackTrace = ex.StackTrace?.Split(Environment.NewLine) ?? Array.Empty<string>();
            var lines = new[] { $"[{ex.GetType().Name}] {ex.Message}" }.Concat(stackTrace);

            return lines.ToArray();
        }

        #endregion
    }
}
