using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Nameless.AdventureWorks.IdentityServer.Api.v1.Validations {
    public sealed class CreateUserValidation : AbstractValidator<CreateUserInput> {
        #region Public Constructors

        public CreateUserValidation(UserManager<User> userManager) {
            Prevent.Against.Null(userManager, nameof(userManager));

            RuleFor(_ => _.FirstName)
                .NotEmpty();
            
            RuleFor(_ => _.LastName)
                .NotEmpty();

            RuleFor(_ => _.UserName)
                .NotEmpty();

            RuleFor(_ => _.Password)
                .NotEmpty()
                .Equal(_ => _.ConfirmPassword);

            RuleFor(_ => _.Email)
                .NotEmpty()
                .MustAsync(async (email, cancellationToken) => {
                    return await userManager.Users.CountAsync(_ => _.Email == email, cancellationToken) == 0;
                }).WithMessage("E-mail already registered.");
        }

        #endregion
    }
}
