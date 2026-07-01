using MediatR;
using Moq;
using ReelRating.Application.Handler.CommentsHandler.ManageHandler;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;

namespace ReelRating.Tests.Handlers.ManageDeleteCommentsTests;

public class ManageDeleteCommentsHandlerTests
{
    private readonly Mock<IManageCommentService> _manageCommentServiceMock;
    private readonly ManageDeleteCommentsHandler _manageDeleteCommentsHandler;

    public ManageDeleteCommentsHandlerTests()
    {
        _manageCommentServiceMock = new Mock<IManageCommentService>();
        _manageDeleteCommentsHandler = new ManageDeleteCommentsHandler(_manageCommentServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCommentDeletedSuccessfully()
    {
        // Arrange
        var request = new DeleteCommentRequest { Id = 1 };
        var expectedResult = new Result();
        expectedResult.SetSuccess(true);

        _manageCommentServiceMock
            .Setup(x => x.DeleteComment(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageDeleteCommentsHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _manageCommentServiceMock.Verify(x => x.DeleteComment(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCommentNotFound()
    {
        // Arrange
        var request = new DeleteCommentRequest { Id = 99 };
        var expectedResult = new Result();
        expectedResult.ValidateResult("Comment with id '99' not found.", 404);

        _manageCommentServiceMock
            .Setup(x => x.DeleteComment(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageDeleteCommentsHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _manageCommentServiceMock.Verify(x => x.DeleteComment(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new DeleteCommentRequest { Id = 1 };
        var expectedResult = new Result();

        _manageCommentServiceMock
            .Setup(x => x.DeleteComment(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _manageDeleteCommentsHandler.Handle(request, CancellationToken.None);

        // Assert
        _manageCommentServiceMock.Verify(x => x.DeleteComment(request), Times.Once);
    }
}
