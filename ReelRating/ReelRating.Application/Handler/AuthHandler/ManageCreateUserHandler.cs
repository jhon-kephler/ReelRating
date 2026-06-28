using MediatR;
using ReelRating.Application.Services.AuthServices.Interfaces;
using ReelRating.Application.Services.HomeServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.AuthHandler
{
    public class ManageCreateUserHandler : IRequestHandler<CreateRequest, Result<bool>>
    {
        private readonly IManageAuthService _manageAuthService;

        public ManageCreateUserHandler(IManageAuthService manageAuthService)
        {
            _manageAuthService = manageAuthService;
        }

        public async Task<Result<bool>> Handle(CreateRequest request, CancellationToken cancellationToken) =>
                                await _manageAuthService.CreateUserAsync(request);
    }
}
