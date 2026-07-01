using Moq;
using ReelRating.Application.Services.AuthServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;

namespace ReelRating.Tests.Mock.AuthMock;

public static class IManageAuthServiceMock
{
    public static Mock<IManageAuthService> Create()
    {
        return new Mock<IManageAuthService>();
    }

    public static Mock<IManageAuthService> SetupCreateUserAsync(this Mock<IManageAuthService> mock, CreateRequest request, Result<bool> result)
    {
        mock.Setup(x => x.CreateUserAsync(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageAuthService> SetupCreateUserAsyncSuccess(this Mock<IManageAuthService> mock, CreateRequest request)
    {
        var result = new Result<bool>();
        result.SetSuccess(true);
        mock.Setup(x => x.CreateUserAsync(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageAuthService> SetupCreateUserAsyncError(this Mock<IManageAuthService> mock, CreateRequest request, string errorMessage)
    {
        var result = new Result<bool>();
        result.ValidateResult(errorMessage);
        mock.Setup(x => x.CreateUserAsync(request)).ReturnsAsync(result);
        return mock;
    }
}
