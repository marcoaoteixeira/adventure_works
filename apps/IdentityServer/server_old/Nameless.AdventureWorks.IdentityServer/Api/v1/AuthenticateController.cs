using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nameless.AdventureWorks.Extensions;

namespace Nameless.AdventureWorks.IdentityServer.Api.v1 {
    public sealed class AuthenticateController : ApiControllerBase {
        #region Private Read-Only Fields

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IValidator<AuthenticationInput> _authenticationInputValidator;

        #endregion

        #region Public Constructors

        public AuthenticateController(IAuthenticationService authenticationService, IMapper mapper, IValidator<AuthenticationInput> authenticationInputValidator) {
            _authenticationService = Prevent.Against.Null(authenticationService, nameof(authenticationService));
            _mapper = Prevent.Against.Null(mapper, nameof(mapper));
            _authenticationInputValidator = Prevent.Against.Null(authenticationInputValidator, nameof(authenticationInputValidator));
        }

        #endregion

        #region Public Methods

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] AuthenticationInput input, CancellationToken cancellationToken = default) {
            var validate = await _authenticationInputValidator.ValidateAsync(input, cancellationToken);
            if (!validate.Success()) {
                validate.Fill(ModelState);

                return BadRequest(ModelState);
            }

            var request = new AuthenticationRequest(input.Identifier, input.Password, HttpContext.GetIPv4());
            var authenticate = await _authenticationService.AuthenticateAsync(request, cancellationToken);
            var output = _mapper.Map<AuthenticationOutput>(authenticate);

            return authenticate.Successful ? Ok(output) : BadRequest(authenticate.Error);
        }

        #endregion
    }
}
