using MediatR;
using ReelRating.Application.Services.HomeServices;
using ReelRating.Application.Services.HomeServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.HomeSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.HomeHandler
{
    public class SearchAllCategoriesHandler : IRequestHandler<CategoriesRequest, PaginationResult<CategoriesResponse>>
    {
        private readonly ISearchFiltersServices _searchHomeServices;

        public SearchAllCategoriesHandler(ISearchFiltersServices searchHomeServices)
        {
            _searchHomeServices = searchHomeServices;
        }

        public async Task<PaginationResult<CategoriesResponse>> Handle(CategoriesRequest request, CancellationToken cancellationToken) =>
                                await _searchHomeServices.GetCategories(request);
    }
}
