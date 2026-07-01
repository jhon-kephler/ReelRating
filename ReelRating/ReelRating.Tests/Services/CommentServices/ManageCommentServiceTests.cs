using AutoMapper;
using Moq;
using ReelRating.Application.Services.CommentServices;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Data.Command.CommentCommand;
using ReelRating.Data.Query.CommetsQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Tests.Services.ManageCommentTests;

public class ManageCommentServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICreateCommentCommand> _createCommentCommandMock;
    private readonly Mock<IUpdateCommentCommand> _updateCommentCommandMock;
    private readonly Mock<IGetCommentByIdQuery> _getCommentByIdQueryMock;
    private readonly ManageCommentService _manageCommentService;

    public ManageCommentServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _createCommentCommandMock = new Mock<ICreateCommentCommand>();
        _updateCommentCommandMock = new Mock<IUpdateCommentCommand>();
        _getCommentByIdQueryMock = new Mock<IGetCommentByIdQuery>();

        _manageCommentService = new ManageCommentService(
            _mapperMock.Object,
            _createCommentCommandMock.Object,
            _updateCommentCommandMock.Object,
            _getCommentByIdQueryMock.Object
        );
    }

    [Fact]
    public async Task CreateComment_ShouldReturnSuccess_WhenValidRequest()
    {
        // Arrange
        var request = new CreateCommentRequest { CommentText = "Test comment", CustomerId = 1, CineId = 1 };
        var comment = new Comments { Id = 1, CommentText = "Test comment" };

        _mapperMock.Setup(x => x.Map<Comments>(request)).Returns(comment);
        _createCommentCommandMock.Setup(x => x.CreateAsync(comment)).ReturnsAsync(true);

        // Act
        var result = await _manageCommentService.CreateComment(request);

        // Assert
        Assert.True(result.IsSuccess);
        _mapperMock.Verify(x => x.Map<Comments>(request), Times.Once);
        _createCommentCommandMock.Verify(x => x.CreateAsync(comment), Times.Once);
    }

    [Fact]
    public async Task CreateComment_ShouldReturnError_WhenExceptionThrown()
    {
        // Arrange
        var request = new CreateCommentRequest { CommentText = "Test comment", CustomerId = 1, CineId = 1 };
        _mapperMock.Setup(x => x.Map<Comments>(request)).Throws(new Exception("Database error"));

        // Act
        var result = await _manageCommentService.CreateComment(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("error", result.ErrorMessage.ToLower());
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnSuccess_WhenCommentExists()
    {
        // Arrange
        var request = new DeleteCommentRequest
        {
            Id = 1
        };

        var comment = new Comments
        {
            Id = 1,
            CommentText = "Test comment",
            Deleted = false
        };

        _getCommentByIdQueryMock
            .Setup(x => x.GetCommentById(request.Id))
            .ReturnsAsync(comment);

        _updateCommentCommandMock
            .Setup(x => x.UpdateCommets(request.Id, It.IsAny<Comments>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _manageCommentService.DeleteComment(request);

        // Assert
        Assert.True(result.IsSuccess);

        _getCommentByIdQueryMock.Verify(
            x => x.GetCommentById(request.Id),
            Times.Once);

        _updateCommentCommandMock.Verify( x => x.UpdateCommets( request.Id, It.Is<Comments>(c => c.Deleted.Value &&  c.Id == request.Id && c.CommentText == "Test comment")), Times.Once);
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnError_WhenCommentNotFound()
    {
        // Arrange
        var request = new DeleteCommentRequest { Id = 1 };
        _getCommentByIdQueryMock.Setup(x => x.GetCommentById(request.Id)).ReturnsAsync((Comments?)null);

        // Act
        var result = await _manageCommentService.DeleteComment(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
        _updateCommentCommandMock.Verify(x => x.UpdateCommets(It.IsAny<int>(), It.IsAny<Comments>()), Times.Never);
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnError_WhenExceptionThrown()
    {
        // Arrange
        var request = new DeleteCommentRequest { Id = 1 };
        _getCommentByIdQueryMock.Setup(x => x.GetCommentById(request.Id)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _manageCommentService.DeleteComment(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("error", result.ErrorMessage.ToLower());
    }
}
