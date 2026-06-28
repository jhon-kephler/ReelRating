using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.AuthSchema.Response;

namespace ReelRating.Application.Services.AuthServices.Interfaces
{
    public interface ISearchAuthService
    {
        Task<Result<AuthUserResponse>> SignInAsync(SignInRequest request);
    }
}