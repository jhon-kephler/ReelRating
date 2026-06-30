using MediatR;
using ReelRating.Core.Schema.ReviewSchema.Response;

namespace ReelRating.Core.Schema.ReviewSchema.Request
{
    public class ListReviewByCustomerIdRequest : IRequest<PaginationResult<ReviewResponse>>
    {
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
