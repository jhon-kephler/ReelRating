using MediatR;
using Moq;
using ReelRating.Application.Handler.CineHandler;
using ReelRating.Application.Services.CineServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CineSchema.Request;
using ReelRating.Core.Schema.CineSchema.Response;

namespace ReelRating.Tests.Handlers;

public class SearchCineByNameHandlerTests
{
    private readonly Mock<ISearchCineServices> _searchCineServicesMock;
    private readonly SearchCineByNameHandler _searchCineByNameHandler;

    public SearchCineByNameHandlerTests()
    {
        _searchCineServicesMock = new Mock<ISearchCineServices>();
        _searchCineByNameHandler = new SearchCineByNameHandler(_searchCineServicesMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult_WhenRequestIsValid()
    {
        // Arrange
        var request = new CineRequest { Name = "Test Movie" };
        var expectedResult = new Result<CineResponse>();
        expectedResult.SetSuccess(new CineResponse { Id = 1, Name = "Test Movie" });

        _searchCineServicesMock
            .Setup(x => x.SearchCineByName(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchCineByNameHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        _searchCineServicesMock.Verify(x => x.SearchCineByName(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new CineRequest { Name = "Test Movie" };
        var expectedResult = new Result<CineResponse>();

        _searchCineServicesMock
            .Setup(x => x.SearchCineByName(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _searchCineByNameHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchCineServicesMock.Verify(x => x.SearchCineByName(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenServiceReturnsError()
    {
        // Arrange
        var request = new CineRequest { Name = "Nonexistent Movie" };
        var expectedResult = new Result<CineResponse>();
        expectedResult.ValidateResult("Cine not found");

        _searchCineServicesMock
            .Setup(x => x.SearchCineByName(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchCineByNameHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        _searchCineServicesMock.Verify(x => x.SearchCineByName(request), Times.Once);
    }
}
