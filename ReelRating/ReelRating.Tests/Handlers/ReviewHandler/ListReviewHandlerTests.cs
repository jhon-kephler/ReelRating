using MediatR;
using Moq;
using ReelRating.Application.Handler.ReviewHandler.SearchHandler;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using ReelRating.Core.Schema.ReviewSchema.Response;

namespace ReelRating.Tests.Handlers.ListReviewTests;

public class ListReviewHandlerTests
{
    private readonly Mock<ISearchReviewService> _searchReviewServiceMock;
    private readonly ListReviewHandler _listReviewHandler;

    public ListReviewHandlerTests()
    {
        _searchReviewServiceMock = new Mock<ISearchReviewService>();
        _listReviewHandler = new ListReviewHandler(_searchReviewServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginationResult_WhenReviewsExist()
    {
        // Arrange
        var request = new ListReviewByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<ReviewResponse>();
        expectedResult.SetSuccess(new List<ReviewResponse> { new() { Id = 1, Note = 5 } }, 1, 10, 1);

        _searchReviewServiceMock
            .Setup(x => x.ListReviews(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _listReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _searchReviewServiceMock.Verify(x => x.ListReviews(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenReviewsNotFound()
    {
        // Arrange
        var request = new ListReviewByCustomerIdRequest { Id = 99, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<ReviewResponse>();
        expectedResult.ValidateResult("Review with id '99' not found.", 404);

        _searchReviewServiceMock
            .Setup(x => x.ListReviews(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _listReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _searchReviewServiceMock.Verify(x => x.ListReviews(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new ListReviewByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<ReviewResponse>();

        _searchReviewServiceMock
            .Setup(x => x.ListReviews(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _listReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchReviewServiceMock.Verify(x => x.ListReviews(request), Times.Once);
    }
}
