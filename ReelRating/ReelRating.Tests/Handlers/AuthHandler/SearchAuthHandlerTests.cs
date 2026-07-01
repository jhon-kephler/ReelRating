using MediatR;
using Moq;
using ReelRating.Application.Handler.AuthHandler;
using ReelRating.Application.Services.AuthServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.AuthSchema.Response;

namespace ReelRating.Tests.Handlers.SearchAuthTests;

public class SearchAuthHandlerTests
{
    private readonly Mock<ISearchAuthService> _searchAuthServiceMock;
    private readonly SearchAuthHandler _searchAuthHandler;

    public SearchAuthHandlerTests()
    {
        _searchAuthServiceMock = new Mock<ISearchAuthService>();
        _searchAuthHandler = new SearchAuthHandler(_searchAuthServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCredentialsAreValid()
    {
        // Arrange
        var request = new SignInRequest { User = "johndoe", Password = "SecurePassword123" };
        var expectedResult = new Result<AuthUserResponse>();
        expectedResult.SetSuccess(new AuthUserResponse { AccessToken = "token123" });

        _searchAuthServiceMock
            .Setup(x => x.SignInAsync(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchAuthHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        _searchAuthServiceMock.Verify(x => x.SignInAsync(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCredentialsAreInvalid()
    {
        // Arrange
        var request = new SignInRequest { User = "johndoe", Password = "WrongPassword" };
        var expectedResult = new Result<AuthUserResponse>();
        expectedResult.ValidateResult("Invalid credentials", 401);

        _searchAuthServiceMock
            .Setup(x => x.SignInAsync(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _searchAuthHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(401, result.StatusCode);
        _searchAuthServiceMock.Verify(x => x.SignInAsync(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new SignInRequest { Email = "john@example.com", Password = "SecurePassword123" };
        var expectedResult = new Result<AuthUserResponse>();

        _searchAuthServiceMock
            .Setup(x => x.SignInAsync(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _searchAuthHandler.Handle(request, CancellationToken.None);

        // Assert
        _searchAuthServiceMock.Verify(x => x.SignInAsync(request), Times.Once);
    }
}
