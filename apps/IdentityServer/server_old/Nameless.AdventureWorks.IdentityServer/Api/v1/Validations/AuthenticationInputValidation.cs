using FluentValidation;

namespace Nameless.AdventureWorks.IdentityServer.Api.v1.Validations {
    public class AuthenticationInputValidation : AbstractValidator<AuthenticationInput> {
        #region Public Constructors

        public AuthenticationInputValidation() {
            RuleFor(_ => _.Identifier)
                .NotEmpty();

            RuleFor(_ => _.Password)
                .NotEmpty();
        }

        #endregion
    }
}
