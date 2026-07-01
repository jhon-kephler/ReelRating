using MediatR;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.ReviewHandler.ManageHandler
{
    public class ManageDeleteReviewHandler : IRequestHandler<DeleteReviewRequest, Result>
    {
        private readonly IManageReviewService _manageReviewService;

        public ManageDeleteReviewHandler(IManageReviewService manageReviewService)
        {
            _manageReviewService = manageReviewService;
        }

        public async Task<Result> Handle(DeleteReviewRequest request, CancellationToken cancellationToken) =>
                                await _manageReviewService.DeleteReview(request);
    }
}
