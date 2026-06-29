using MediatR;
using ReelRating.Application.Services.HomeServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FiltersSchema.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.FiltersHandler
{
    public class SearchAllYearHandler : IRequestHandler<YearRequest, PaginationResult<int?>>
    {
        private readonly ISearchFiltersServices _searchFiltersServices;

        public SearchAllYearHandler(ISearchFiltersServices searchFiltersServices)
        {
            _searchFiltersServices = searchFiltersServices;
        }

        public async Task<PaginationResult<int?>> Handle(YearRequest request, CancellationToken cancellationToken) =>
                                await _searchFiltersServices.GetYear(request);
    }
}
