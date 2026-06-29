using AutoMapper;
using ReelRating.Application.Services.AuthServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.AuthSchema.Response;
using ReelRating.Data.Query.AuthQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Application.Services.AuthServices
{
    public class SearchAuthService : ISearchAuthService
    {
        private readonly IMapper _mapper;
        private readonly IGetCustomerQuery _getCustomerQuery;
        private readonly ITokenService _tokenService;

        public SearchAuthService(IMapper mapper, IGetCustomerQuery getCustomerQuery, ITokenService tokenService)
        {
            _mapper = mapper;
            _getCustomerQuery = getCustomerQuery;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthUserResponse>> SignInAsync(SignInRequest request)
        {
            var result = new Result<AuthUserResponse>();

            if (string.IsNullOrWhiteSpace(request.User) && string.IsNullOrWhiteSpace(request.Email))
            {
                result.ValidateResult("User or Email is required");
                return result;
            }

            Customer? user = null;

            if (!string.IsNullOrWhiteSpace(request.User))
                user = await _getCustomerQuery.GetByUserAsync(request.User);
            else
                user = await _getCustomerQuery.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                result.ValidateResult("Invalid credentials", 401);
                return result;
            }

            var response = BuildResponse(user);

            result.SetSuccess(response);
            return result;
        }

        private AuthUserResponse BuildResponse(Customer user)
        {
            return new AuthUserResponse
            {
                User = _mapper.Map<UserInfo>(user),
                AccessToken = _tokenService.GenerateToken(user),
                ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            };
        }
    }
}
