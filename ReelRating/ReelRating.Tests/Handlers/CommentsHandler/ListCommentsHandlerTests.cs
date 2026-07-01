using MediatR;
using Moq;
using ReelRating.Application.Handler.CommentsHandler.SearchHandler;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;

namespace ReelRating.Tests.Handlers.ListCommentsTests;

public class ListCommentsHandlerTests
{
    private readonly Mock<ISearchCommentsService> _searchCommentsServiceMock;
    private readonly ListCommentsHandler _listCommentsHandler;

    public ListCommentsHandlerTests()
    {
        _searchCommentsServiceMock = new Mock<ISearchCommentsService>();
        _listCommentsHandler = new ListCommentsHandler(_searchCommentsServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginationResult_WhenCommentsExist()
    {
        // Arrange
        var request = new ListCommentsRequest { PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CommentResponse>();
        expectedResult.SetSuccess(new List<CommentResponse> { new() { Id = 1 }, new() { Id = 2 } }, 1, 10, 2);

        _searchCommentsServiceMock
            .Setup(x => x.ListComments(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _listCommentsHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _searchCommentsServiceMock.Verify(x => x.ListComments(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new ListCommentsRequest { PageNumber = 1, PageSize = 10 };
        var expectedResult = new PaginationResult<CommentResponse>();

        _searchCommentsServiceMock
            .Setup(x => x.ListComments(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _listCommentsHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchCommentsServiceMock.Verify(x => x.ListComments(request), Times.Once);
    }
}
