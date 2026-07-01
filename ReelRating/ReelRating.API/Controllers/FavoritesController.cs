using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReelRating.Core.Schema.FavoritesSchema.Request;

namespace ReelRating.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "favorite")]
    public class FavoritesController : Controller
    {
        private readonly ILogger<FavoritesController> _logger;
        private readonly IMediator _mediator;

        public FavoritesController(ILogger<FavoritesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetFavorite([FromQuery] SearchFavoriteByCustomerIdRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Cine")]
        public async Task<IActionResult> GetFavoriteByCineId([FromQuery] SearchFavoriteByCustomerIdAndCineIdRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("ListById")]
        public async Task<IActionResult> ListFavoriteById([FromQuery] ListFavoritesByCustomerIdRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("List")]
        public async Task<IActionResult> ListFavorite([FromQuery] ListFavoritesRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> PostCreate([FromBody] CreateFavoriteRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("Delete")]
        public async Task<IActionResult> PostDelete([FromBody] DeleteFavoriteRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }
    }
}
