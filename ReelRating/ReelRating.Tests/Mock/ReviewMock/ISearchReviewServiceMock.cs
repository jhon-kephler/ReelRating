using Moq;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using ReelRating.Core.Schema.ReviewSchema.Response;

namespace ReelRating.Tests.Mock.ReviewMock;

public static class ISearchReviewServiceMock
{
    public static Mock<ISearchReviewService> Create()
    {
        return new Mock<ISearchReviewService>();
    }

    public static Mock<ISearchReviewService> SetupListReviews(this Mock<ISearchReviewService> mock, ListReviewByCustomerIdRequest request, PaginationResult<ReviewResponse> result)
    {
        mock.Setup(x => x.ListReviews(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<ISearchReviewService> SetupSearchReview(this Mock<ISearchReviewService> mock, SearchReviewByIdRequest request, Result<ReviewResponse> result)
    {
        mock.Setup(x => x.SearchReview(request)).ReturnsAsync(result);
        return mock;
    }
}
