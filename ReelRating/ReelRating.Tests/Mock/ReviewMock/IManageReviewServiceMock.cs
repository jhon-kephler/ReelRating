using Moq;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;

namespace ReelRating.Tests.Mock.ReviewMock;

public static class IManageReviewServiceMock
{
    public static Mock<IManageReviewService> Create()
    {
        return new Mock<IManageReviewService>();
    }

    public static Mock<IManageReviewService> SetupCreateReview(this Mock<IManageReviewService> mock, CreateReviewRequest request, Result result)
    {
        mock.Setup(x => x.CreateReview(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageReviewService> SetupCreateReviewSuccess(this Mock<IManageReviewService> mock, CreateReviewRequest request)
    {
        var result = new Result();
        result.SetSuccess(true);
        mock.Setup(x => x.CreateReview(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageReviewService> SetupUpdateReview(this Mock<IManageReviewService> mock, UpdateReviewRequest request, Result result)
    {
        mock.Setup(x => x.UpdateReview(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageReviewService> SetupDeleteReview(this Mock<IManageReviewService> mock, DeleteReviewRequest request, Result result)
    {
        mock.Setup(x => x.DeleteReview(request)).ReturnsAsync(result);
        return mock;
    }
}
