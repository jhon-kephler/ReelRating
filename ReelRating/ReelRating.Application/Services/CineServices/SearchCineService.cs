using AutoMapper;
using ReelRating.Application.Services.CineServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CineSchema.Request;
using ReelRating.Core.Schema.CineSchema.Response;
using ReelRating.Data.Query.CineCategoriesQuery;
using ReelRating.Data.Query.CineQuery;
using ReelRating.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.CineServices
{
    public class SearchCineService : ISearchCineServices
    {
        private readonly IMapper _mapper;
        private readonly IListCineByYear _listCineByYear;
        private readonly IGetCineByNameQuery _getCineByNameQuery;
        private readonly IListCineByCategoriesAndYearQuery _listCineByCategoriesAndYear;

        public SearchCineService(IMapper mapper, IListCineByYear listCineByYear, IGetCineByNameQuery getCineByNameQuery, IListCineByCategoriesAndYearQuery listCineByCategoriesAndYear)
        {
            _mapper = mapper;
            _listCineByYear = listCineByYear;
            _getCineByNameQuery = getCineByNameQuery;
            _listCineByCategoriesAndYear = listCineByCategoriesAndYear;
        }

        public async Task<PaginationResult<CineResponse>> SearchCineDefault(ListCineDefaultRequest request)
        {
            var result = new PaginationResult<CineResponse>();
            try
            {
                var cine = await _listCineByYear.ListCineByYearAsync(DateTime.Now.Year, request.PageNumber, request.PageSize);

                var mapped = _mapper.Map<List<CineResponse>>(cine);

                result.SetSuccess(mapped, request.PageNumber, request.PageSize, cine.Count);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return await Task.FromResult(result);
        }

        public async Task<PaginationResult<CineResponse>> SearchCineByFilters(ListCineByFiltersRequest request)
        {
            var result = new PaginationResult<CineResponse>();

            try
            {
                (List<Cine> Items, int Total) cine;

                cine = await _listCineByCategoriesAndYear.ListCineByCategoriesAndYearAsync(request.CategoriesId, request.Year, request.PageNumber, request.PageSize);
            
                var mapped = _mapper.Map<List<CineResponse>>(cine.Items);

                result.SetSuccess(mapped, request.PageNumber, request.PageSize, cine.Total);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }

            return result;
        }

        public async Task<Result<CineResponse>> SearchCineByName(CineRequest request)
        {
            Result<CineResponse> result = new Result<CineResponse>();
            try
            {
                var cine = await _getCineByNameQuery.GetCineByNameAsync(request.Name);
                if(cine == null)
                    throw new Exception($"Cine with name '{request.Name}' not found.");

                result.SetSuccess(_mapper.Map<CineResponse>(cine));
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return await Task.FromResult(result);
        }
    }
}