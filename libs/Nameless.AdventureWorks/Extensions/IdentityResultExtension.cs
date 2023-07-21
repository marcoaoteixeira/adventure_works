using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nameless.AdventureWorks {
    public static class IdentityResultExtension {
        #region Public Static Methods

        // Syntaxt sugar
        public static bool Success(this IdentityResult self)
            => self.Succeeded;

        // Syntaxt sugar
        public static bool Failure(this IdentityResult self)
            => !self.Succeeded;

        public static void Fill(this IdentityResult self, ModelStateDictionary modelState) {
            if (self.Succeeded) { return; }

            foreach (var error in self.Errors) {
                modelState.AddModelError(error.Code, error.Description);
            }
        }

        #endregion
    }
}
