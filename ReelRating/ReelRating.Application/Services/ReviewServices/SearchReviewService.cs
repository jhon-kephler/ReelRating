using AutoMapper;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using ReelRating.Core.Schema.ReviewSchema.Response;
using ReelRating.Data.Query.ReviewQuery;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.ReviewServices
{
    public class SearchReviewService : ISearchReviewService
    {
        private readonly IMapper _mapper;
        private readonly IGetReviewByIdNonDeletedQuery _getReviewByIdNonDeletedQuery;
        private readonly IListReviewByIdQuery _listReviewByIdNonDeletedQuery;

        public SearchReviewService(IMapper mapper, IGetReviewByIdNonDeletedQuery getReviewByIdNonDeletedQuery, IListReviewByIdQuery listReviewByIdNonDeletedQuery)
        {
            _mapper = mapper;
            _getReviewByIdNonDeletedQuery = getReviewByIdNonDeletedQuery;
            _listReviewByIdNonDeletedQuery = listReviewByIdNonDeletedQuery;
        }

        public async Task<PaginationResult<ReviewResponse>> ListReviews(ListReviewByCustomerIdRequest request)
        {
            var result = new PaginationResult<ReviewResponse>();
            try
            {
                var review = await _listReviewByIdNonDeletedQuery.ListReviewById(request.Id, request.PageNumber, request.PageSize);
                if (review == null)
                {
                    result.ValidateResult($"Review with id '{request.Id}' not found.", 404);
                    return result;
                }

                result.SetSuccess(_mapper.Map<List<ReviewResponse>>(review), request.PageNumber, request.PageSize, review.Count);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            
            return result;
        }

        public async Task<Result<ReviewResponse>> SearchReview(SearchReviewByIdRequest request)
        {
            var result = new Result<ReviewResponse>();
            try
            {
                var review = await _getReviewByIdNonDeletedQuery.GetReviewByIdNonDeleted(request.Id);
                if(review == null)
                {
                    result.ValidateResult($"Review with id '{request.Id}' not found.", 404);
                    return result;
                }

                result.SetSuccess(_mapper.Map<ReviewResponse>(review));
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }

            return result;
        }
    }
}