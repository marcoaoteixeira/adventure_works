using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nameless.AdventureWorks.Extensions {
    public static class IdentityResultExtension {
        #region Public Static Methods

        public static void Fill(this IdentityResult self, ModelStateDictionary modelState) {
            if (self.Succeeded) { return; }

            var dictionary = self
                .Errors
                .GroupBy(_ => _.Code)
                .ToDictionary(
                    keySelector: _ => _.Key,
                    elementSelector: _ => _.Select(item => item.Description).ToArray()
                );
            foreach (var kvp in dictionary) {
                var message = string.Join(Environment.NewLine, kvp.Value);
                modelState.AddModelError(kvp.Key, message);
            }
        }

        #endregion
    }
}
