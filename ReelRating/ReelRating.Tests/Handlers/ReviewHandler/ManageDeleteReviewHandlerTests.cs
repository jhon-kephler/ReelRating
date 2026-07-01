using MediatR;
using Moq;
using ReelRating.Application.Handler.ReviewHandler.ManageHandler;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;

namespace ReelRating.Tests.Handlers.ManageDeleteReviewTests;

public class ManageDeleteReviewHandlerTests
{
    private readonly Mock<IManageReviewService> _manageReviewServiceMock;
    private readonly ManageDeleteReviewHandler _manageDeleteReviewHandler;

    public ManageDeleteReviewHandlerTests()
    {
        _manageReviewServiceMock = new Mock<IManageReviewService>();
        _manageDeleteReviewHandler = new ManageDeleteReviewHandler(_manageReviewServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenReviewDeletedSuccessfully()
    {
        // Arrange
        var request = new DeleteReviewRequest { Id = 1 };
        var expectedResult = new Result();
        expectedResult.SetSuccess(true);

        _manageReviewServiceMock
            .Setup(x => x.DeleteReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageDeleteReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _manageReviewServiceMock.Verify(x => x.DeleteReview(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenReviewNotFound()
    {
        // Arrange
        var request = new DeleteReviewRequest { Id = 99 };
        var expectedResult = new Result();
        expectedResult.ValidateResult("Review with id '99' not found.", 404);

        _manageReviewServiceMock
            .Setup(x => x.DeleteReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageDeleteReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _manageReviewServiceMock.Verify(x => x.DeleteReview(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new DeleteReviewRequest { Id = 1 };
        var expectedResult = new Result();

        _manageReviewServiceMock
            .Setup(x => x.DeleteReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _manageDeleteReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        _manageReviewServiceMock.Verify(x => x.DeleteReview(request), Times.Once);
    }
}
