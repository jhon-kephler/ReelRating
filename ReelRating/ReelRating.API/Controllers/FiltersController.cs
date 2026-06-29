using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.HomeSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Response;

namespace ReelRating.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FiltersController : Controller
    {
        private readonly ILogger<FiltersController> _logger;
        private readonly IMediator _mediator;

        public FiltersController(ILogger<FiltersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories([FromQuery] CategoriesRequest request)
        {
            var result = await _mediator.Send(request);
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { error = result.ErrorMessage });

            return Ok(result);
        }

        //[HttpGet("Year")]
        //public Task<Result<string>> GetYear() =>
        //    _mediator.Send();
    }
}