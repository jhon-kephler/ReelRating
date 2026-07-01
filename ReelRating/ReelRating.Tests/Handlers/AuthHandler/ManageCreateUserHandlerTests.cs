using MediatR;
using Moq;
using ReelRating.Application.Handler.AuthHandler;
using ReelRating.Application.Services.AuthServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;

namespace ReelRating.Tests.Handlers;

public class ManageCreateUserHandlerTests
{
    private readonly Mock<IManageAuthService> _manageAuthServiceMock;
    private readonly ManageCreateUserHandler _manageCreateUserHandler;

    public ManageCreateUserHandlerTests()
    {
        _manageAuthServiceMock = new Mock<IManageAuthService>();
        _manageCreateUserHandler = new ManageCreateUserHandler(_manageAuthServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUserCreatedSuccessfully()
    {
        // Arrange
        var request = new CreateRequest
        {
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = "SecurePassword123"
        };
        var expectedResult = new Result<bool>();
        expectedResult.SetSuccess(true);

        _manageAuthServiceMock
            .Setup(x => x.CreateUserAsync(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageCreateUserHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.True(result.Data);
        _manageAuthServiceMock.Verify(x => x.CreateUserAsync(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserCreationFails()
    {
        // Arrange
        var request = new CreateRequest
        {
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = "SecurePassword123"
        };
        var expectedResult = new Result<bool>();
        expectedResult.ValidateResult("Email already exists");

        _manageAuthServiceMock
            .Setup(x => x.CreateUserAsync(request))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _manageCreateUserHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Contains("email already exists", result.ErrorMessage.ToLower());
        _manageAuthServiceMock.Verify(x => x.CreateUserAsync(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallService_WhenRequestIsReceived()
    {
        // Arrange
        var request = new CreateRequest
        {
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = "SecurePassword123"
        };
        var expectedResult = new Result<bool>();

        _manageAuthServiceMock
            .Setup(x => x.CreateUserAsync(request))
            .ReturnsAsync(expectedResult);

        // Act
        await _manageCreateUserHandler.Handle(request, CancellationToken.None);

        // Assert
        _manageAuthServiceMock.Verify(x => x.CreateUserAsync(request), Times.Once);
    }
}
