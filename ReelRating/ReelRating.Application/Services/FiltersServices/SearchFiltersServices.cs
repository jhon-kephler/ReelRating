using AutoMapper;
using MediatR;
using ReelRating.Application.Services.HomeServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.HomeSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Response;
using ReelRating.Data.Query.FiltersQuery;

namespace ReelRating.Application.Services.HomeServices
{
    public class SearchFiltersServices : ISearchFiltersServices
    {
        public readonly IMapper _mapper;
        public readonly IGetListCategories _getListCategories;

        public SearchFiltersServices(IMapper mapper, IGetListCategories getListCategories)
        {
            _mapper = mapper;
            _getListCategories = getListCategories;
        }

        public async Task<PaginationResult<CategoriesResponse>> GetCategories(CategoriesRequest request)
        {
            var result = new PaginationResult<CategoriesResponse>();
            try
            {
                var categories = _mapper.Map<List<CategoriesResponse>>(await _getListCategories.GetAllAsync());
                result.SetSuccess(categories, request.PageSize, request.TotalPages, request.TotalItems);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred while fetching categories: {ex.Message}");
            }
            return result;
        }
    }
}
