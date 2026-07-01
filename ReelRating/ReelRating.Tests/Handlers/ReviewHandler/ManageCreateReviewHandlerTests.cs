using MediatR;
using Moq;
using ReelRating.Application.Handler.ReviewHandler.SearchHandler;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;

namespace ReelRating.Tests.Handlers.ManageCreateReviewTests;

public class ManageCreateReviewHandlerTests
{
    private readonly Mock<IManageReviewService> _manageReviewServiceMock;
    private readonly CreateReviewHandler _manageCreateReviewHandler;

    public ManageCreateReviewHandlerTests()
    {
        _manageReviewServiceMock = new Mock<IManageReviewService>();
        _manageCreateReviewHandler = new CreateReviewHandler(_manageReviewServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenReviewCreatedSuccessfully()
    {
        // Arrange
        var request = new CreateReviewRequest { CineId = 1, CustomerId = 1, REVIEW = "Great movie", Note = 5 };
        var expectedResult = new Result();
        expectedResult.SetSuccess(true);

        _manageReviewServiceMock
            .Setup(x => x.CreateReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageCreateReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _manageReviewServiceMock.Verify(x => x.CreateReview(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenReviewCreationFails()
    {
        // Arrange
        var request = new CreateReviewRequest { CineId = 1, CustomerId = 1, REVIEW = "Great movie", Note = 5 };
        var expectedResult = new Result();
        expectedResult.ValidateResult("Invalid request");

        _manageReviewServiceMock
            .Setup(x => x.CreateReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageCreateReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _manageReviewServiceMock.Verify(x => x.CreateReview(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new CreateReviewRequest { CineId = 1, CustomerId = 1, REVIEW = "Great movie", Note = 5 };
        var expectedResult = new Result();

        _manageReviewServiceMock
            .Setup(x => x.CreateReview(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _manageCreateReviewHandler.Handle(request, CancellationToken.None);

        // Assert
        _manageReviewServiceMock.Verify(x => x.CreateReview(request), Times.Once);
    }
}
