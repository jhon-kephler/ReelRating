using AutoMapper;
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

        public ManageAuthService(IMapper mapper, ICreateCommand createCommand)
        {
            _mapper = mapper;
            _createCommand = createCommand;
        }

        public async Task<Result<bool>> CreateUserAsync(CreateRequest request)
        {
            var result = new Result<bool>();

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                result.ValidateResult("Password is required");
                return result;
            }

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _createCommand.CreateAsync(_mapper.Map<Customer>(request));

            result.SetSuccess(true);
            return result;
        }
    }
}
