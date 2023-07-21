using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nameless.AdventureWorks {
    public static class ValidationResultExtension {
        #region Public Static Methods

        // Syntaxt sugar
        public static bool Success(this ValidationResult self)
            => self.IsValid;

        // Syntaxt sugar
        public static bool Failure(this ValidationResult self)
            => !self.IsValid;

        public static void Fill(this ValidationResult self, ModelStateDictionary modelState) {
            if (!self.IsValid) {
                foreach (var item in self.Errors) {
                    modelState.AddModelError(item.ErrorCode, item.ErrorMessage);
                }
            }
        }

        #endregion
    }
}
