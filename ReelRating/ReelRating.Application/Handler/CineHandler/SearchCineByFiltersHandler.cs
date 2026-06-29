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
    public class SearchCineByFiltersHandler : IRequestHandler<ListCineByFiltersRequest, PaginationResult<CineResponse>>
    {
        private readonly ISearchCineServices _searchCineServices;

        public SearchCineByFiltersHandler(ISearchCineServices searchCineServices)
        {
            _searchCineServices = searchCineServices;
        }

        public async Task<PaginationResult<CineResponse>> Handle(ListCineByFiltersRequest request, CancellationToken cancellationToken) =>
                                await _searchCineServices.SearchCineByFilters(request);
    }
}
