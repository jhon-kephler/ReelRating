using AutoMapper;
using Moq;
using ReelRating.Application.Services.ReviewServices;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using ReelRating.Data.Command.ReviewCommand;
using ReelRating.Data.Query.CineQuery;
using ReelRating.Data.Query.CustomerQuery;
using ReelRating.Data.Query.ReviewQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Tests.Services.ManageReviewTests;

public class ManageReviewServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICreateReviewCommand> _createReviewCommandMock;
    private readonly Mock<IGetCineByIdQuery> _getCineByIdQueryMock;
    private readonly Mock<IGetCustomerByIdQuery> _getCustomerByIdQueryMock;
    private readonly Mock<IGetReviewByIdQuery> _getReviewByIdQueryMock;
    private readonly Mock<IUpdateReviewCommand> _updateReviewCommandMock;
    private readonly ManageReviewService _manageReviewService;

    public ManageReviewServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _createReviewCommandMock = new Mock<ICreateReviewCommand>();
        _getCineByIdQueryMock = new Mock<IGetCineByIdQuery>();
        _getCustomerByIdQueryMock = new Mock<IGetCustomerByIdQuery>();
        _getReviewByIdQueryMock = new Mock<IGetReviewByIdQuery>();
        _updateReviewCommandMock = new Mock<IUpdateReviewCommand>();

        _manageReviewService = new ManageReviewService(
            _mapperMock.Object,
            _createReviewCommandMock.Object,
            _getCineByIdQueryMock.Object,
            _getCustomerByIdQueryMock.Object,
            _getReviewByIdQueryMock.Object,
            _updateReviewCommandMock.Object
        );
    }

    [Fact]
    public async Task CreateReview_ShouldReturnSuccess_WhenValidRequest()
    {
        // Arrange
        var request = new CreateReviewRequest { CineId = 1, CustomerId = 1, REVIEW = "Great movie", Note = 5 };
        var cine = new Cine { Id = 1, Name = "Test Movie" };
        var customer = new Customer { Id = 1, Name = "John Doe" };
        var review = new Review { Id = 1, REVIEW = "Great movie", Note = 5 };

        _getCineByIdQueryMock.Setup(x => x.GetCineByIdAsync(request.CineId)).ReturnsAsync(cine);
        _getCustomerByIdQueryMock.Setup(x => x.GetCustomerById(request.CustomerId)).ReturnsAsync(customer);
        _mapperMock.Setup(x => x.Map<Review>(request)).Returns(review);
        _createReviewCommandMock.Setup(x => x.AddReview(review)).Returns(Task.CompletedTask);

        // Act
        var result = await _manageReviewService.CreateReview(request);

        // Assert
        Assert.True(result.IsSuccess);
        _getCineByIdQueryMock.Verify(x => x.GetCineByIdAsync(request.CineId), Times.Once);
        _getCustomerByIdQueryMock.Verify(x => x.GetCustomerById(request.CustomerId), Times.Once);
        _createReviewCommandMock.Verify(x => x.AddReview(review), Times.Once);
    }

    [Fact]
    public async Task CreateReview_ShouldReturnError_WhenCineNotFound()
    {
        // Arrange
        var request = new CreateReviewRequest { CineId = 1, CustomerId = 1, REVIEW = "Great movie", Note = 5 };

        _getCineByIdQueryMock.Setup(x => x.GetCineByIdAsync(request.CineId)).ReturnsAsync((Cine?)null);
        _getCustomerByIdQueryMock.Setup(x => x.GetCustomerById(request.CustomerId)).ReturnsAsync(new Customer { Id = 1 });

        // Act
        var result = await _manageReviewService.CreateReview(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("invalid request", result.ErrorMessage.ToLower());
        _createReviewCommandMock.Verify(x => x.AddReview(It.IsAny<Review>()), Times.Never);
    }

    [Fact]
    public async Task CreateReview_ShouldReturnError_WhenCustomerNotFound()
    {
        // Arrange
        var request = new CreateReviewRequest { CineId = 1, CustomerId = 1, REVIEW = "Great movie", Note = 5 };

        _getCineByIdQueryMock.Setup(x => x.GetCineByIdAsync(request.CineId)).ReturnsAsync(new Cine { Id = 1 });
        _getCustomerByIdQueryMock.Setup(x => x.GetCustomerById(request.CustomerId)).ReturnsAsync((Customer?)null);

        // Act
        var result = await _manageReviewService.CreateReview(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("invalid request", result.ErrorMessage.ToLower());
        _createReviewCommandMock.Verify(x => x.AddReview(It.IsAny<Review>()), Times.Never);
    }

    [Fact]
    public async Task UpdateReview_ShouldReturnSuccess_WhenReviewExists()
    {
        // Arrange
        var request = new UpdateReviewRequest { Id = 1, REVIEW = "Updated review", Note = 4 };
        var review = new Review { Id = 1, REVIEW = "Old review", Note = 5 };

        _getReviewByIdQueryMock.Setup(x => x.GetReviewById(request.Id)).ReturnsAsync(review);
        _updateReviewCommandMock.Setup(x => x.UpdateReview(request.Id, It.IsAny<Review>())).Returns(Task.CompletedTask);

        // Act
        var result = await _manageReviewService.UpdateReview(request);

        // Assert
        Assert.True(result.IsSuccess);
        _getReviewByIdQueryMock.Verify(x => x.GetReviewById(request.Id), Times.Once);
        _updateReviewCommandMock.Verify(x => x.UpdateReview(request.Id, It.Is<Review>(r => r.REVIEW == request.REVIEW && r.Note == request.Note)), Times.Once);
    }

    [Fact]
    public async Task UpdateReview_ShouldReturnError_WhenReviewNotFound()
    {
        // Arrange
        var request = new UpdateReviewRequest { Id = 1, REVIEW = "Updated review", Note = 4 };
        _getReviewByIdQueryMock.Setup(x => x.GetReviewById(request.Id)).ReturnsAsync((Review?)null);

        // Act
        var result = await _manageReviewService.UpdateReview(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
        _updateReviewCommandMock.Verify(x => x.UpdateReview(It.IsAny<int>(), It.IsAny<Review>()), Times.Never);
    }

    [Fact]
    public async Task DeleteReview_ShouldReturnSuccess_WhenReviewExists()
    {
        // Arrange
        var request = new DeleteReviewRequest { Id = 1 };
        var review = new Review { Id = 1, REVIEW = "Great movie", Note = 5, Deleted = false };

        _getReviewByIdQueryMock.Setup(x => x.GetReviewById(request.Id)).ReturnsAsync(review);
        _updateReviewCommandMock.Setup(x => x.UpdateReview(request.Id, It.IsAny<Review>())).Returns(Task.CompletedTask);

        // Act
        var result = await _manageReviewService.DeleteReview(request);

        // Assert
        Assert.True(result.IsSuccess);
        _getReviewByIdQueryMock.Verify(x => x.GetReviewById(request.Id), Times.Once);
        _updateReviewCommandMock.Verify(x => x.UpdateReview(request.Id, It.Is<Review>(r => r.Deleted == true)), Times.Once);
    }

    [Fact]
    public async Task DeleteReview_ShouldReturnError_WhenReviewNotFound()
    {
        // Arrange
        var request = new DeleteReviewRequest { Id = 1 };
        _getReviewByIdQueryMock.Setup(x => x.GetReviewById(request.Id)).ReturnsAsync((Review?)null);

        // Act
        var result = await _manageReviewService.DeleteReview(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("not found", result.ErrorMessage.ToLower());
        _updateReviewCommandMock.Verify(x => x.UpdateReview(It.IsAny<int>(), It.IsAny<Review>()), Times.Never);
    }

    [Fact]
    public async Task CreateReview_ShouldReturnError_WhenExceptionThrown()
    {
        // Arrange
        var request = new CreateReviewRequest { CineId = 1, CustomerId = 1, REVIEW = "Great movie", Note = 5 };
        _getCineByIdQueryMock.Setup(x => x.GetCineByIdAsync(request.CineId)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _manageReviewService.CreateReview(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("error", result.ErrorMessage.ToLower());
    }
}
