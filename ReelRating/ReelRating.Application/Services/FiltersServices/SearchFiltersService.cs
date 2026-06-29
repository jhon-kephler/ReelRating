using AutoMapper;
using MediatR;
using ReelRating.Application.Services.HomeServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FiltersSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Response;
using ReelRating.Data.Query.CineQuery;
using ReelRating.Data.Query.FiltersQuery;

namespace ReelRating.Application.Services.HomeServices
{
    public class SearchFiltersService : ISearchFiltersServices
    {
        public readonly IMapper _mapper;
        public readonly IListCategories _listCategories;
        public readonly IListYearQuery _listYearQuery;

        public SearchFiltersService(IMapper mapper, IListCategories listCategories, IListYearQuery listYearQuery)
        {
            _mapper = mapper;
            _listCategories = listCategories;
            _listYearQuery = listYearQuery;
        }

        public async Task<PaginationResult<CategoriesResponse>> GetCategories(CategoriesRequest request)
        {
            var result = new PaginationResult<CategoriesResponse>();
            try
            {
                var categories = _mapper.Map<List<CategoriesResponse>>(await _listCategories.GetAllAsync(request.PageNumber, request.PageSize));
                result.SetSuccess(categories, request.PageNumber, request.PageSize, request.TotalItems);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred while fetching categories: {ex.Message}");
            }
            return result;
        }

        public async Task<PaginationResult<int?>> GetYear(YearRequest request)
        {
            var result = new PaginationResult<int?>();
            try
            {
                var years = await _listYearQuery.GetAllYearAsync(request.PageNumber, request.PageSize);
                result.SetSuccess(years, request.PageNumber, request.PageSize, request.TotalItems);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred while fetching years: {ex.Message}");
            }
            return result;
        }
    }
}
