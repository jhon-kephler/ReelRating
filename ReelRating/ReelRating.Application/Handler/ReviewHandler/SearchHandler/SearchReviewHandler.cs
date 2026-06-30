using MediatR;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using ReelRating.Core.Schema.ReviewSchema.Response;

namespace ReelRating.Application.Handler.ReviewHandler.SearchHandler
{
    public class SearchReviewHandler : IRequestHandler<SearchReviewByIdRequest, Result<ReviewResponse>>
    {
        private readonly ISearchReviewService _searchReviewService;

        public SearchReviewHandler(ISearchReviewService searchReviewService)
        {
            _searchReviewService = searchReviewService;
        }

        public async Task<Result<ReviewResponse>> Handle(SearchReviewByIdRequest request, CancellationToken cancellationToken) =>
                                await _searchReviewService.SearchReview(request);
    }
}
