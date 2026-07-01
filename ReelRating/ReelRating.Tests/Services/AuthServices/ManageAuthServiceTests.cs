using AutoMapper;
using Moq;
using ReelRating.Application.Services.AuthServices;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Data.Command.AuthCommand;
using ReelRating.Data.Query.AuthQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Tests.Services;

public class ManageAuthServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICreateCommand> _createCommandMock;
    private readonly Mock<IGetCustomerQuery> _getCustomerQueryMock;
    private readonly ManageAuthService _manageAuthService;

    public ManageAuthServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _createCommandMock = new Mock<ICreateCommand>();
        _getCustomerQueryMock = new Mock<IGetCustomerQuery>();

        _manageAuthService = new ManageAuthService(
            _mapperMock.Object,
            _createCommandMock.Object,
            _getCustomerQueryMock.Object
        );
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnSuccess_WhenValidUser()
    {
        // Arrange
        var request = new CreateRequest
        {
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = "SecurePassword123"
        };
        var customer = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = "hashedPassword"
        };

        _getCustomerQueryMock.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync((Customer?)null);
        _getCustomerQueryMock.Setup(x => x.GetByNickNameAsync(request.Nickname)).ReturnsAsync((Customer?)null);
        _mapperMock.Setup(x => x.Map<Customer>(request)).Returns(customer);
        _createCommandMock.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(true);

        // Act
        var result = await _manageAuthService.CreateUserAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Data);
        _getCustomerQueryMock.Verify(x => x.GetByEmailAsync(request.Email), Times.Once);
        _getCustomerQueryMock.Verify(x => x.GetByNickNameAsync(request.Nickname), Times.Once);
        _createCommandMock.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Once);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnError_WhenEmailAlreadyExists()
    {
        // Arrange
        var request = new CreateRequest
        {
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = "SecurePassword123"
        };
        var existingCustomer = new Customer
        {
            Id = 1,
            Email = "john@example.com"
        };

        _getCustomerQueryMock.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync(existingCustomer);

        // Act
        var result = await _manageAuthService.CreateUserAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("email already exists", result.ErrorMessage.ToLower());
        _getCustomerQueryMock.Verify(x => x.GetByEmailAsync(request.Email), Times.Once);
        _createCommandMock.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnError_WhenNicknameAlreadyExists()
    {
        // Arrange
        var request = new CreateRequest
        {
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = "SecurePassword123"
        };
        var existingCustomer = new Customer
        {
            Id = 1,
            Nickname = "johndoe"
        };

        _getCustomerQueryMock.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync((Customer?)null);
        _getCustomerQueryMock.Setup(x => x.GetByNickNameAsync(request.Nickname)).ReturnsAsync(existingCustomer);

        // Act
        var result = await _manageAuthService.CreateUserAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("nickname already exists", result.ErrorMessage.ToLower());
        _getCustomerQueryMock.Verify(x => x.GetByEmailAsync(request.Email), Times.Once);
        _getCustomerQueryMock.Verify(x => x.GetByNickNameAsync(request.Nickname), Times.Once);
        _createCommandMock.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnError_WhenPasswordIsEmpty()
    {
        // Arrange
        var request = new CreateRequest
        {
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = ""
        };

        _getCustomerQueryMock.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync((Customer?)null);
        _getCustomerQueryMock.Setup(x => x.GetByNickNameAsync(request.Nickname)).ReturnsAsync((Customer?)null);

        // Act
        var result = await _manageAuthService.CreateUserAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("password", result.ErrorMessage.ToLower());
        _createCommandMock.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnError_WhenPasswordIsNull()
    {
        // Arrange
        var request = new CreateRequest
        {
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = null
        };

        _getCustomerQueryMock.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync((Customer?)null);
        _getCustomerQueryMock.Setup(x => x.GetByNickNameAsync(request.Nickname)).ReturnsAsync((Customer?)null);

        // Act
        var result = await _manageAuthService.CreateUserAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("password", result.ErrorMessage.ToLower());
        _createCommandMock.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnError_WhenPasswordIsWhitespace()
    {
        // Arrange
        var request = new CreateRequest
        {
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = "   "
        };

        _getCustomerQueryMock.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync((Customer?)null);
        _getCustomerQueryMock.Setup(x => x.GetByNickNameAsync(request.Nickname)).ReturnsAsync((Customer?)null);

        // Act
        var result = await _manageAuthService.CreateUserAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("password", result.ErrorMessage.ToLower());
        _createCommandMock.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldHashPassword_WhenValidUser()
    {
        // Arrange
        var request = new CreateRequest
        {
            Name = "John Doe",
            Nickname = "johndoe",
            Email = "john@example.com",
            Password = "SecurePassword123"
        };

        var originalPassword = request.Password;

        var customer = new Customer
        {
            Id = 1,
            Name = request.Name,
            Nickname = request.Nickname,
            Email = request.Email
        };

        _getCustomerQueryMock.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync((Customer?)null);
        _getCustomerQueryMock.Setup(x => x.GetByNickNameAsync(request.Nickname)).ReturnsAsync((Customer?)null);

        _mapperMock
            .Setup(x => x.Map<Customer>(It.IsAny<CreateRequest>()))
            .Returns<CreateRequest>(r => new Customer
            {
                Name = r.Name,
                Nickname = r.Nickname,
                Email = r.Email,
                Password = r.Password
            });

        _createCommandMock.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(true);

        var result = await _manageAuthService.CreateUserAsync(request);

        Assert.True(result.IsSuccess);

        _mapperMock.Verify(x => x.Map<Customer>(It.Is<CreateRequest>(r => BCrypt.Net.BCrypt.Verify(originalPassword, r.Password))), Times.Once);

        _createCommandMock.Verify(x => x.CreateAsync(It.Is<Customer>(c => BCrypt.Net.BCrypt.Verify(originalPassword, c.Password))), Times.Once);
    }
}
