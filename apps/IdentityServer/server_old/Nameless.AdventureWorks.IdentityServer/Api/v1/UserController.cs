using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nameless.AdventureWorks.Extensions;
using NuGet.Protocol;

namespace Nameless.AdventureWorks.IdentityServer.Api.v1 {
    [Authorize]
    public sealed class UserController : ApiControllerBase {
        #region Private Read-Only Fields

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserInput> _createUserValidator;

        #endregion

        #region Public Constructors

        public UserController(UserManager<User> userManager, IMapper mapper, IValidator<CreateUserInput> createUserValidator) {
            _userManager = Prevent.Against.Null(userManager, nameof(userManager));
            _mapper = Prevent.Against.Null(mapper, nameof(mapper));
            _createUserValidator = Prevent.Against.Null(createUserValidator, nameof(createUserValidator));
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(string userName) {
            var user = await _userManager.FindByNameAsync(userName);

            return user != null
                ? Ok(new { user.Id })
                : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody]CreateUserInput input, CancellationToken cancellationToken) {
            var validation = await _createUserValidator.ValidateAsync(input, cancellationToken);
            if (validation.Failure()) {
                validation.Fill(ModelState);
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(input);
            var result = await _userManager.CreateAsync(user, input.Password!);

            if (!result.Succeeded) {
                result.Fill(ModelState);
                return BadRequest(ModelState);
            }

            var output = _mapper.Map<CreateUserOutput>(user);

            return Ok(output);
        }

        #endregion
    }
}
