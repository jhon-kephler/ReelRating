using MediatR;
using ReelRating.Application.Services.AuthServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.AuthSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.AuthHandler
{
    public class SearchAuthHandler : IRequestHandler<SignInRequest, Result<AuthUserResponse>>
    {
        private readonly ISearchAuthService _searchAuthService;

        public SearchAuthHandler(ISearchAuthService searchAuthService)
        {
            _searchAuthService = searchAuthService;
        }

        public async Task<Result<AuthUserResponse>> Handle(SignInRequest request, CancellationToken cancellationToken) =>
                                await _searchAuthService.SignInAsync(request);
    }
}
