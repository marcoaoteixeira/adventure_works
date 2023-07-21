﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nameless.AdventureWorks.Filters {
    /// <summary>
    /// Implementation of <see cref="ActionFilterAttribute"/> that validates
    /// if the incoming model is valid.
    /// </summary>
    public sealed class ValidateModelStateActionFilter : ActionFilterAttribute {
        #region Public Override Methods

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context) {
            if (!context.ModelState.IsValid) {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        #endregion
    }
}
