using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReelRating.Core.Schema.CommentsSchema.Request;

namespace ReelRating.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "comments")]
    public class CommentsController : Controller
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly IMediator _mediator;

        public CommentsController(ILogger<CommentsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetComment([FromQuery] SearchCommentByCustomerIdRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Cine")]
        public async Task<IActionResult> GetCommentByCineId([FromQuery] SearchCommentByCustomerIdAndCineIdRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("ListById")]
        public async Task<IActionResult> ListCommentById([FromQuery] ListCommentsByCustomerIdRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("List")]
        public async Task<IActionResult> ListComment([FromQuery] ListCommentsRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> PostCreate([FromBody] CreateCommentRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("Delete")]
        public async Task<IActionResult> PostDelete([FromBody] DeleteCommentRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }
    }
}
