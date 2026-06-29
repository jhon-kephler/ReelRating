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
        private readonly IListCineCategoriesByIdQuery _listCineCategoriesByIdQuery;
        private readonly IListCineByListIdQuery _listCineByListIdQuery;

        public SearchCineService(IMapper mapper, IListCineByYear listCineByYear, IGetCineByNameQuery getCineByNameQuery, IListCineCategoriesByIdQuery listCineCategoriesByIdQuery, IListCineByListIdQuery listCineByListIdQuery)
        {
            _mapper = mapper;
            _listCineByYear = listCineByYear;
            _getCineByNameQuery = getCineByNameQuery;
            _listCineCategoriesByIdQuery = listCineCategoriesByIdQuery;
            _listCineByListIdQuery = listCineByListIdQuery;
        }

        public async Task<PaginationResult<CineResponse>> SearchCineDefault(ListCineDefaultRequest request)
        {
            var result = new PaginationResult<CineResponse>();
            try
            {
                var cine = await _listCineByYear.ListCineByYearAsync(DateTime.Now.Year);

                var totalItems = cine.Count;

                var paged = cine
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var mapped = _mapper.Map<List<CineResponse>>(paged);

                result.SetSuccess(mapped, request.PageNumber, request.PageSize, totalItems);
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
                List<Cine> cine;

                if (request.CategoriesId.HasValue)
                {
                    var cineCategories = await _listCineCategoriesByIdQuery
                        .ListCineCategoriesByIdAsync(request.CategoriesId.Value);

                    var cineIds = cineCategories
                        .Select(c => c.CineId)
                        .Distinct()
                        .ToList();

                    cine = await _listCineByListIdQuery.ListCineByListIdAsync(cineIds);
                }
                else
                {
                    cine = await _listCineByYear.ListCineByYearAsync(request.Year.Value);
                }

                if (request.Year.HasValue)
                    cine = cine.Where(c => c.Year == request.Year.Value).ToList();

                var totalItems = cine.Count;

                var paged = cine
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var mapped = _mapper.Map<List<CineResponse>>(paged);

                result.SetSuccess(mapped, request.PageNumber, request.PageSize, totalItems);
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