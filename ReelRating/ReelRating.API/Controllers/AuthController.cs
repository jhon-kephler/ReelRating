using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Request;

namespace ReelRating.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "authentication")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;

        public AuthController(ILogger<AuthController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> PostSignIn([FromBody] SignInRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> PostCreate([FromBody] CreateRequest request)
        {
            var result = await _mediator.Send(request);
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { error = result.ErrorMessage });

            return Created();
        }

    }
}
