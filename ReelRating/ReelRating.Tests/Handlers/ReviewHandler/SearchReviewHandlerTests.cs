using MediatR;
using Moq;
using ReelRating.Application.Handler.ReviewHandler.SearchHandler;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using ReelRating.Core.Schema.ReviewSchema.Response;

namespace ReelRating.Tests.Handlers.SearchReviewTests;

public class SearchReviewHandlerTests
{
    private readonly Mock<ISearchReviewService> _searchReviewServiceMock;
    private readonly SearchReviewHandler _searchReviewHandler;

    public SearchReviewHandlerTests()
    {
        _searchReviewServiceMock = new Mock<ISearchReviewService>();
        _searchReviewHandler = new SearchReviewHandler(_searchReviewServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenReviewFound()
    {
        // Arrange
        var request = new SearchReviewByIdRequest { Id = 1 };
        var expectedResult = new Result<ReviewResponse>();
        expectedResult.SetSuccess(new ReviewResponse { Id = 1, Note = 5 });

        _searchReviewServiceMock
            .Setup(x => x.SearchReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        _searchReviewServiceMock.Verify(x => x.SearchReview(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenReviewNotFound()
    {
        // Arrange
        var request = new SearchReviewByIdRequest { Id = 99 };
        var expectedResult = new Result<ReviewResponse>();
        expectedResult.ValidateResult("Review with id '99' not found.", 404);

        _searchReviewServiceMock
            .Setup(x => x.SearchReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _searchReviewServiceMock.Verify(x => x.SearchReview(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new SearchReviewByIdRequest { Id = 1 };
        var expectedResult = new Result<ReviewResponse>();

        _searchReviewServiceMock
            .Setup(x => x.SearchReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _searchReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchReviewServiceMock.Verify(x => x.SearchReview(request), Times.Once);
    }
}
