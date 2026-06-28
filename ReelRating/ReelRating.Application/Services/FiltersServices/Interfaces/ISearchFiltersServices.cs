using ReelRating.Core.Schema;
using ReelRating.Core.Schema.HomeSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.HomeServices.Interfaces
{
    public interface ISearchFiltersServices
    {
        Task<PaginationResult<CategoriesResponse>> GetCategories(CategoriesRequest request);
    }
}
