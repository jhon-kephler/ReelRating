using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.CineSchema.Request;
using ReelRating.Core.Schema.CineSchema.Response;

namespace ReelRating.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "cine")]
    public class CineController : Controller
    {
        private readonly ILogger<CineController> _logger;
        private readonly IMediator _mediator;

        public CineController(ILogger<CineController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("Name")]
        public async Task<IActionResult> GetCineByName([FromQuery] CineRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Filters")]
        public async Task<IActionResult> GetCineByFilters([FromQuery] ListCineByFiltersRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetCineDefault([FromQuery] ListCineDefaultRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }
    }
}