using MediatR;
using Moq;
using ReelRating.Application.Handler.ReviewHandler.ManageHandler;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;

namespace ReelRating.Tests.Handlers.ManageUpdateReviewTests;

public class ManageUpdateReviewHandlerTests
{
    private readonly Mock<IManageReviewService> _manageReviewServiceMock;
    private readonly ManageUpdateReviewHandler _manageUpdateReviewHandler;

    public ManageUpdateReviewHandlerTests()
    {
        _manageReviewServiceMock = new Mock<IManageReviewService>();
        _manageUpdateReviewHandler = new ManageUpdateReviewHandler(_manageReviewServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenReviewUpdatedSuccessfully()
    {
        // Arrange
        var request = new UpdateReviewRequest { Id = 1, REVIEW = "Updated review", Note = 4 };
        var expectedResult = new Result();
        expectedResult.SetSuccess(true);

        _manageReviewServiceMock
            .Setup(x => x.UpdateReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageUpdateReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _manageReviewServiceMock.Verify(x => x.UpdateReview(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenReviewNotFound()
    {
        // Arrange
        var request = new UpdateReviewRequest { Id = 99, REVIEW = "Updated review", Note = 4 };
        var expectedResult = new Result();
        expectedResult.ValidateResult("Review with id '99' not found.", 404);

        _manageReviewServiceMock
            .Setup(x => x.UpdateReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageUpdateReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _manageReviewServiceMock.Verify(x => x.UpdateReview(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new UpdateReviewRequest { Id = 1, REVIEW = "Updated review", Note = 4 };
        var expectedResult = new Result();

        _manageReviewServiceMock
            .Setup(x => x.UpdateReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _manageUpdateReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        _manageReviewServiceMock.Verify(x => x.UpdateReview(request), Times.Once);
    }
}
