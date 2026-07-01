using MediatR;
using Moq;
using ReelRating.Application.Handler.CommentsHandler.SearchHandler;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;

namespace ReelRating.Tests.Handlers.SearchCommentByCustomerIdTests;

public class SearchCommentByCustomerIdHandlerTests
{
    private readonly Mock<ISearchCommentsService> _searchCommentsServiceMock;
    private readonly SearchCommentByCustomerIdHandler _searchCommentByCustomerIdHandler;

    public SearchCommentByCustomerIdHandlerTests()
    {
        _searchCommentsServiceMock = new Mock<ISearchCommentsService>();
        _searchCommentByCustomerIdHandler = new SearchCommentByCustomerIdHandler(_searchCommentsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCommentFound()
    {
        // Arrange
        var request = new SearchCommentsByCustomerIdRequest { Id = 1, CustomerId = 1 };
        var expectedResult = new Result<CommentResponse>();
        expectedResult.SetSuccess(new CommentResponse { Id = 1, CommentText = "Test comment" });

        _searchCommentsServiceMock
            .Setup(x => x.SearchCommentById(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchCommentByCustomerIdHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        _searchCommentsServiceMock.Verify(x => x.SearchCommentById(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCommentNotFound()
    {
        // Arrange
        var request = new SearchCommentsByCustomerIdRequest { Id = 99, CustomerId = 1 };
        var expectedResult = new Result<CommentResponse>();
        expectedResult.ValidateResult("Comment with id '99' not found.", 404);

        _searchCommentsServiceMock
            .Setup(x => x.SearchCommentById(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchCommentByCustomerIdHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _searchCommentsServiceMock.Verify(x => x.SearchCommentById(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new SearchCommentsByCustomerIdRequest { Id = 1, CustomerId = 1 };
        var expectedResult = new Result<CommentResponse>();

        _searchCommentsServiceMock
            .Setup(x => x.SearchCommentById(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _searchCommentByCustomerIdHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchCommentsServiceMock.Verify(x => x.SearchCommentById(request), Times.Once);
    }
}
