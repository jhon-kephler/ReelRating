using AutoMapper;
using Moq;
using ReelRating.Application.Services.CommentServices;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;
using ReelRating.Data.Query.CommetsQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Tests.Services.SearchCommentsTests;

public class SearchCommentsServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IListCommentsQuery> _listCommentsQueryMock;
    private readonly Mock<IGetCommentByIdQuery> _getCommentByIdQueryMock;
    private readonly SearchCommentsService _searchCommentsService;

    public SearchCommentsServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _listCommentsQueryMock = new Mock<IListCommentsQuery>();
        _getCommentByIdQueryMock = new Mock<IGetCommentByIdQuery>();

        _searchCommentsService = new SearchCommentsService(
            _mapperMock.Object,
            _listCommentsQueryMock.Object,
            _getCommentByIdQueryMock.Object
        );
    }

    [Fact]
    public async Task SearchCommentById_ShouldReturnSuccess_WhenCommentFound()
    {
        // Arrange
        var request = new SearchCommentsByCustomerIdRequest { Id = 1, CustomerId = 1 };
        var comment = new Comments { Id = 1, CommentText = "Test comment" };
        var commentResponse = new CommentResponse { Id = 1, CommentText = "Test comment" };

        _getCommentByIdQueryMock
            .Setup(x => x.GetCommentByIdAndCustomerId(request.Id, request.CustomerId))
            .ReturnsAsync(comment);

        _mapperMock
            .Setup(x => x.Map<CommentResponse>(comment))
            .Returns(commentResponse);

        // Act
        var result = await _searchCommentsService.SearchCommentById(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
        _getCommentByIdQueryMock.Verify(x => x.GetCommentByIdAndCustomerId(request.Id, request.CustomerId), Times.Once);
    }

    [Fact]
    public async Task SearchCommentById_ShouldReturnError_WhenCommentNotFound()
    {
        // Arrange
        var request = new SearchCommentsByCustomerIdRequest { Id = 1, CustomerId = 1 };
        _getCommentByIdQueryMock
            .Setup(x => x.GetCommentByIdAndCustomerId(request.Id, request.CustomerId))
            .ReturnsAsync((Comments?)null);

        // Act
        var result = await _searchCommentsService.SearchCommentById(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
    }

    [Fact]
    public async Task SearchCommentByIdAndCineId_ShouldReturnSuccess_WhenCommentFound()
    {
        // Arrange
        var request = new SearchCommentByCustomerIdAndCineIdRequest { Id = 1, CustomerId = 1, CineId = 1 };
        var comment = new Comments { Id = 1, CommentText = "Test comment" };
        var commentResponse = new CommentResponse { Id = 1, CommentText = "Test comment" };

        _getCommentByIdQueryMock
            .Setup(x => x.GetCommentByIdAndCustomerIdAndCineId(request.Id, request.CustomerId, request.CineId))
            .ReturnsAsync(comment);

        _mapperMock
            .Setup(x => x.Map<CommentResponse>(comment))
            .Returns(commentResponse);

        // Act
        var result = await _searchCommentsService.SearchCommentByIdAndCineId(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        _getCommentByIdQueryMock.Verify(x => x.GetCommentByIdAndCustomerIdAndCineId(request.Id, request.CustomerId, request.CineId), Times.Once);
    }

    [Fact]
    public async Task ListComments_ShouldReturnSuccess_WhenCommentsExist()
    {
        // Arrange
        var request = new ListCommentsRequest { PageNumber = 1, PageSize = 10 };
        var comments = new List<Comments>
        {
            new() { Id = 1, CommentText = "Comment 1" },
            new() { Id = 2, CommentText = "Comment 2" }
        };
        var commentResponses = new List<CommentResponse>
        {
            new() { Id = 1, CommentText = "Comment 1" },
            new() { Id = 2, CommentText = "Comment 2" }
        };

        _listCommentsQueryMock
            .Setup(x => x.ListComments(request.PageNumber, request.PageSize))
            .ReturnsAsync(comments);

        _mapperMock
            .Setup(x => x.Map<List<CommentResponse>>(comments))
            .Returns(commentResponses);

        // Act
        var result = await _searchCommentsService.ListComments(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);
        _listCommentsQueryMock.Verify(x => x.ListComments(request.PageNumber, request.PageSize), Times.Once);
    }

    [Fact]
    public async Task ListCommentsById_ShouldReturnSuccess_WhenCommentsExist()
    {
        // Arrange
        var request = new ListCommentsByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        var comments = new List<Comments>
        {
            new() { Id = 1, CommentText = "Comment 1" }
        };
        var commentResponses = new List<CommentResponse>
        {
            new() { Id = 1, CommentText = "Comment 1" }
        };

        _listCommentsQueryMock
            .Setup(x => x.ListCommentsById(request.Id, request.PageNumber, request.PageSize))
            .ReturnsAsync(comments);

        _mapperMock
            .Setup(x => x.Map<List<CommentResponse>>(comments))
            .Returns(commentResponses);

        // Act
        var result = await _searchCommentsService.ListCommentsById(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Single(result.Data);
        _listCommentsQueryMock.Verify(x => x.ListCommentsById(request.Id, request.PageNumber, request.PageSize), Times.Once);
    }

    [Fact]
    public async Task ListCommentsById_ShouldReturnError_WhenCommentsNotFound()
    {
        // Arrange
        var request = new ListCommentsByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        _listCommentsQueryMock
            .Setup(x => x.ListCommentsById(request.Id, request.PageNumber, request.PageSize))
            .ReturnsAsync((List<Comments>?)null);

        // Act
        var result = await _searchCommentsService.ListCommentsById(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
    }
}
