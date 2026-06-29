using MediatR;
using ReelRating.Application.Services.CineServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CineSchema.Request;
using ReelRating.Core.Schema.CineSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.CineHandler
{
    public class SearchCineByNameHandler : IRequestHandler<CineRequest, Result<CineResponse>>
    {
        private readonly ISearchCineServices _searchCineServices;

        public SearchCineByNameHandler(ISearchCineServices searchCineServices)
        {
            _searchCineServices = searchCineServices;
        }

        public async Task<Result<CineResponse>> Handle(CineRequest request, CancellationToken cancellationToken) =>
                                await _searchCineServices.SearchCineByName(request);
    }
}