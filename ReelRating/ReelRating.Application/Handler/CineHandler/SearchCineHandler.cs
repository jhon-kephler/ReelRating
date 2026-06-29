using MediatR;
using ReelRating.Application.Services.CineServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CineSchema.Request;
using ReelRating.Core.Schema.CineSchema.Response;
using ReelRating.Core.Schema.HomeSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.CineHandler
{
    public class SearchCineHandler : IRequestHandler<ListCineDefaultRequest, PaginationResult<CineResponse>>
    {
        private readonly ISearchCineServices _searchCineServices;

        public SearchCineHandler(ISearchCineServices searchCineServices)
        {
            _searchCineServices = searchCineServices;
        }

        public async Task<PaginationResult<CineResponse>> Handle(ListCineDefaultRequest request, CancellationToken cancellationToken) =>
                                await _searchCineServices.SearchCineDefault(request);
    }
}
