using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using ReelRating.Core.Schema.ReviewSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.ReviewServices.Interfaces
{
    public interface ISearchReviewService
    {
        Task<PaginationResult<ReviewResponse>> ListReviews(ListReviewByCustomerIdRequest request);
        Task<Result<ReviewResponse>> SearchReview(SearchReviewByIdRequest request);
    }
}
