using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CineSchema.Request;
using ReelRating.Core.Schema.CineSchema.Response;

namespace ReelRating.Application.Services.CineServices.Interfaces
{
    public interface ISearchCineServices
    {
        Task<Result<CineResponse>> SearchCineByName(CineRequest request);
        Task<PaginationResult<CineResponse>> SearchCineByFilters(ListCineByFiltersRequest request);
        Task<PaginationResult<CineResponse>> SearchCineDefault(ListCineDefaultRequest request);
    }
}