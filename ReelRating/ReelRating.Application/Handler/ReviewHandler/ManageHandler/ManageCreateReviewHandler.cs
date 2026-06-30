using MediatR;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.ReviewSchema.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.ReviewHandler.SearchHandler
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewRequest, Result<bool>>
    {
        private readonly IManageReviewService _manageReviewService;

        public CreateReviewHandler(IManageReviewService manageReviewService)
        {
            _manageReviewService = manageReviewService;
        }
        public async Task<Result<bool>> Handle(CreateReviewRequest request, CancellationToken cancellationToken) =>
                                await _manageReviewService.CreateReview(request);
    }
}
