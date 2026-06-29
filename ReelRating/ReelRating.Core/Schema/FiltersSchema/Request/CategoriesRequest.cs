using MediatR;
using ReelRating.Core.Schema.HomeSchema.Response;
using System.Text.Json.Serialization;

namespace ReelRating.Core.Schema.HomeSchema.Request
{
    public class CategoriesRequest : IRequest<PaginationResult<CategoriesResponse>>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PageNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int PageSize { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int TotalItems { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalItems / PageSize) : 0;
    }
}
