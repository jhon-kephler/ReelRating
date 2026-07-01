using MediatR;
using Moq;
using ReelRating.Application.Handler.CommentsHandler.SearchHandler;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;

namespace ReelRating.Tests.Handlers.SearchCommentByCustomerIdAndCineIdTests;

public class SearchCommentByCustomerIdAndCineIdHandlerTests
{
    private readonly Mock<ISearchCommentsService> _searchCommentsServiceMock;
    private readonly SearchCommentByCustomerIdAndCineIdHandler _searchCommentByCustomerIdAndCineIdHandler;

    public SearchCommentByCustomerIdAndCineIdHandlerTests()
    {
        _searchCommentsServiceMock = new Mock<ISearchCommentsService>();
        _searchCommentByCustomerIdAndCineIdHandler = new SearchCommentByCustomerIdAndCineIdHandler(_searchCommentsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCommentFound()
    {
        // Arrange
        var request = new SearchCommentByCustomerIdAndCineIdRequest { Id = 1, CustomerId = 1, CineId = 1 };
        var expectedResult = new Result<CommentResponse>();
        expectedResult.SetSuccess(new CommentResponse { Id = 1, CommentText = "Test comment" });

        _searchCommentsServiceMock
            .Setup(x => x.SearchCommentByIdAndCineId(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchCommentByCustomerIdAndCineIdHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        _searchCommentsServiceMock.Verify(x => x.SearchCommentByIdAndCineId(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCommentNotFound()
    {
        // Arrange
        var request = new SearchCommentByCustomerIdAndCineIdRequest { Id = 99, CustomerId = 1, CineId = 1 };
        var expectedResult = new Result<CommentResponse>();
        expectedResult.ValidateResult("Comment with id '99' not found.", 404);

        _searchCommentsServiceMock
            .Setup(x => x.SearchCommentByIdAndCineId(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchCommentByCustomerIdAndCineIdHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _searchCommentsServiceMock.Verify(x => x.SearchCommentByIdAndCineId(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new SearchCommentByCustomerIdAndCineIdRequest { Id = 1, CustomerId = 1, CineId = 1 };
        var expectedResult = new Result<CommentResponse>();

        _searchCommentsServiceMock
            .Setup(x => x.SearchCommentByIdAndCineId(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _searchCommentByCustomerIdAndCineIdHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchCommentsServiceMock.Verify(x => x.SearchCommentByIdAndCineId(request), Times.Once);
    }
}
