using MediatR;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.ReviewSchema.Request;
using ReelRating.Core.Schema.ReviewSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.ReviewHandler.SearchHandler
{
    public class ListReviewHandler : IRequestHandler<ListReviewByCustomerIdRequest, PaginationResult<ReviewResponse>>
    {
        private readonly ISearchReviewService _searchReviewService;

        public ListReviewHandler(ISearchReviewService searchReviewService)
        {
            _searchReviewService = searchReviewService;
        }

        public async Task<PaginationResult<ReviewResponse>> Handle(ListReviewByCustomerIdRequest request, CancellationToken cancellationToken) =>
                                await _searchReviewService.ListReviews(request);
    }
}
