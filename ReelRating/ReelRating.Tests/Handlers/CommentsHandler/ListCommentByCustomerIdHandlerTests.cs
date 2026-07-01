using MediatR;
using Moq;
using ReelRating.Application.Handler.CommentsHandler.SearchHandler;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;

namespace ReelRating.Tests.Handlers.ListCommentByCustomerIdTests;

public class ListCommentByCustomerIdHandlerTests
{
    private readonly Mock<ISearchCommentsService> _searchCommentsServiceMock;
    private readonly ListCommentByCustomerIdHandler _listCommentByCustomerIdHandler;

    public ListCommentByCustomerIdHandlerTests()
    {
        _searchCommentsServiceMock = new Mock<ISearchCommentsService>();
        _listCommentByCustomerIdHandler = new ListCommentByCustomerIdHandler(_searchCommentsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginationResult_WhenCommentsExist()
    {
        // Arrange
        var request = new ListCommentsByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CommentResponse>();
        expectedResult.SetSuccess(new List<CommentResponse> { new() { Id = 1, CommentText = "Test" } }, 1, 10, 1);

        _searchCommentsServiceMock
            .Setup(x => x.ListCommentsById(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _listCommentByCustomerIdHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _searchCommentsServiceMock.Verify(x => x.ListCommentsById(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCommentsNotFound()
    {
        // Arrange
        var request = new ListCommentsByCustomerIdRequest { Id = 99, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CommentResponse>();
        expectedResult.ValidateResult("List Comment with id '99' not found.", 404);

        _searchCommentsServiceMock
            .Setup(x => x.ListCommentsById(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _listCommentByCustomerIdHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _searchCommentsServiceMock.Verify(x => x.ListCommentsById(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new ListCommentsByCustomerIdRequest { Id = 1, PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CommentResponse>();

        _searchCommentsServiceMock
            .Setup(x => x.ListCommentsById(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _listCommentByCustomerIdHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchCommentsServiceMock.Verify(x => x.ListCommentsById(request), Times.Once);
    }
}
