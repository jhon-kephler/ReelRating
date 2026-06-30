using AutoMapper;
using Org.BouncyCastle.Asn1.Ocsp;
using ReelRating.Application.Services.AuthServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.AuthSchema.Response;
using ReelRating.Data.Command.AuthCommand;
using ReelRating.Data.Query.AuthQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Application.Services.AuthServices
{
    public class ManageAuthService : IManageAuthService
    {
        private readonly IMapper _mapper;
        private readonly ICreateCommand _createCommand;
        private readonly IGetCustomerQuery _getCustomerQuery;

        public ManageAuthService(IMapper mapper, ICreateCommand createCommand, IGetCustomerQuery getCustomerQuery)
        {
            _mapper = mapper;
            _createCommand = createCommand;
            _getCustomerQuery = getCustomerQuery;
        }

        public async Task<Result<bool>> CreateUserAsync(CreateRequest request)
        {
            var result = new Result<bool>();

            var validationResult = await ValidateUser(request);
            if (validationResult.IsSuccess == false)
            {
                result.ValidateResult(validationResult.ErrorMessage);
                return result;
            }

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _createCommand.CreateAsync(_mapper.Map<Customer>(request));

            result.SetSuccess(true);

            return result;
        }

        private async Task<Result> ValidateUser(CreateRequest request)
        {
            var result = new Result();

            var email = await _getCustomerQuery.GetByEmailAsync(request.Email);
            if (email != null)
            {
                result.ValidateResult("Email already exists");
                return result;
            }

            var nickName = await _getCustomerQuery.GetByNickNameAsync(request.Email);
            if (nickName != null)
            {
                result.ValidateResult("nickName already exists");
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                result.ValidateResult("Password is required");
                return result;
            }

            result.SetSuccess(true);

            return result;
        }
    }
}