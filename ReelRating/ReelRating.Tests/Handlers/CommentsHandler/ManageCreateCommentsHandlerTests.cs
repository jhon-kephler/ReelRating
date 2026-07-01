using MediatR;
using Moq;
using ReelRating.Application.Handler.CommentsHandler.ManageHandler;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;

namespace ReelRating.Tests.Handlers.ManageCreateCommentsTests;

public class ManageCreateCommentsHandlerTests
{
    private readonly Mock<IManageCommentService> _manageCommentServiceMock;
    private readonly ManageCreateCommentsHandler _manageCreateCommentsHandler;

    public ManageCreateCommentsHandlerTests()
    {
        _manageCommentServiceMock = new Mock<IManageCommentService>();
        _manageCreateCommentsHandler = new ManageCreateCommentsHandler(_manageCommentServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCommentCreatedSuccessfully()
    {
        // Arrange
        var request = new CreateCommentRequest { CommentText = "Test comment", CustomerId = 1, CineId = 1 };
        var expectedResult = new Result();
        expectedResult.SetSuccess(true);

        _manageCommentServiceMock
            .Setup(x => x.CreateComment(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageCreateCommentsHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _manageCommentServiceMock.Verify(x => x.CreateComment(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCommentCreationFails()
    {
        // Arrange
        var request = new CreateCommentRequest { CommentText = "Test comment", CustomerId = 1, CineId = 1 };
        var expectedResult = new Result();
        expectedResult.ValidateResult("Error creating comment");

        _manageCommentServiceMock
            .Setup(x => x.CreateComment(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageCreateCommentsHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _manageCommentServiceMock.Verify(x => x.CreateComment(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new CreateCommentRequest { CommentText = "Test comment", CustomerId = 1, CineId = 1 };
        var expectedResult = new Result();

        _manageCommentServiceMock
            .Setup(x => x.CreateComment(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _manageCreateCommentsHandler.Handle(request, CancellationToken.None);

        // Assert
        _manageCommentServiceMock.Verify(x => x.CreateComment(request), Times.Once);
    }
}
