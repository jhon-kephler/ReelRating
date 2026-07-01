using AutoMapper;
using Moq;
using ReelRating.Application.Services.ReviewServices;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using ReelRating.Core.Schema.ReviewSchema.Response;
using ReelRating.Data.Query.ReviewQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Tests.Services.SearchReviewTests;

public class SearchReviewServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IGetReviewByIdNonDeletedQuery> _getReviewByIdNonDeletedQueryMock;
    private readonly Mock<IListReviewByIdQuery> _listReviewByIdNonDeletedQueryMock;
    private readonly SearchReviewService _searchReviewService;

    public SearchReviewServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _getReviewByIdNonDeletedQueryMock = new Mock<IGetReviewByIdNonDeletedQuery>();
        _listReviewByIdNonDeletedQueryMock = new Mock<IListReviewByIdQuery>();

        _searchReviewService = new SearchReviewService(
            _mapperMock.Object,
            _getReviewByIdNonDeletedQueryMock.Object,
            _listReviewByIdNonDeletedQueryMock.Object
        );
    }

    [Fact]
    public async Task ListReviews_ShouldReturnSuccess_WhenReviewsExist()
    {
        // Arrange
        var request = new ListReviewByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        var reviews = new List<Review>
        {
            new() { Id = 1, REVIEW = "Great movie", Note = 5 },
            new() { Id = 2, REVIEW = "Good movie", Note = 4 }
        };
        var reviewResponses = new List<ReviewResponse>
        {
            new() { Id = 1, REVIEW = "Great movie", Note = 5 },
            new() { Id = 2, REVIEW = "Good movie", Note = 4 }
        };

        _listReviewByIdNonDeletedQueryMock
            .Setup(x => x.ListReviewById(request.Id, request.PageNumber, request.PageSize))
            .ReturnsAsync(reviews);

        _mapperMock
            .Setup(x => x.Map<List<ReviewResponse>>(reviews))
            .Returns(reviewResponses);

        // Act
        var result = await _searchReviewService.ListReviews(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);
        _listReviewByIdNonDeletedQueryMock.Verify(x => x.ListReviewById(request.Id, request.PageNumber, request.PageSize), Times.Once);
    }

    [Fact]
    public async Task ListReviews_ShouldReturnError_WhenReviewsNotFound()
    {
        // Arrange
        var request = new ListReviewByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        _listReviewByIdNonDeletedQueryMock
            .Setup(x => x.ListReviewById(request.Id, request.PageNumber, request.PageSize))
            .ReturnsAsync((List<Review>?)null);

        // Act
        var result = await _searchReviewService.ListReviews(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
    }

    [Fact]
    public async Task SearchReview_ShouldReturnSuccess_WhenReviewFound()
    {
        // Arrange
        var request = new SearchReviewByIdRequest { Id = 1 };
        var review = new Review { Id = 1, REVIEW = "Great movie", Note = 5 };
        var reviewResponse = new ReviewResponse { Id = 1, REVIEW = "Great movie", Note = 5 };

        _getReviewByIdNonDeletedQueryMock
            .Setup(x => x.GetReviewByIdNonDeleted(request.Id))
            .ReturnsAsync(review);

        _mapperMock
            .Setup(x => x.Map<ReviewResponse>(review))
            .Returns(reviewResponse);

        // Act
        var result = await _searchReviewService.SearchReview(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
        _getReviewByIdNonDeletedQueryMock.Verify(x => x.GetReviewByIdNonDeleted(request.Id), Times.Once);
    }

    [Fact]
    public async Task SearchReview_ShouldReturnError_WhenReviewNotFound()
    {
        // Arrange
        var request = new SearchReviewByIdRequest { Id = 1 };
        _getReviewByIdNonDeletedQueryMock
            .Setup(x => x.GetReviewByIdNonDeleted(request.Id))
            .ReturnsAsync((Review?)null);

        // Act
        var result = await _searchReviewService.SearchReview(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
    }

    [Fact]
    public async Task SearchReview_ShouldReturnError_WhenExceptionThrown()
    {
        // Arrange
        var request = new SearchReviewByIdRequest { Id = 1 };
        _getReviewByIdNonDeletedQueryMock
            .Setup(x => x.GetReviewByIdNonDeleted(request.Id))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _searchReviewService.SearchReview(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("error", result.ErrorMessage.ToLower());
    }
}
