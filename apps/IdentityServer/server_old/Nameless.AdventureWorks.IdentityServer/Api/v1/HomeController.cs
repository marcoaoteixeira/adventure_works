using Microsoft.AspNetCore.Mvc;

namespace Nameless.AdventureWorks.IdentityServer.Api.v1
{
    public sealed class HomeController : ApiControllerBase {
        #region Public Methods

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get() {
            return Ok(new { message = "It works!" });
        }

        #endregion
    }
}
