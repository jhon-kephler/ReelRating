using MediatR;
using ReelRating.Application.Services.ReviewServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.ReviewHandler.ManageHandler
{
    public class ManageUpdateReviewHandler : IRequestHandler<UpdateReviewRequest, Result<bool>>
    {
        private readonly IManageReviewService _manageReviewService;

        public ManageUpdateReviewHandler(IManageReviewService manageReviewService)
        {
            _manageReviewService = manageReviewService;
        }

        public async Task<Result<bool>> Handle(UpdateReviewRequest request, CancellationToken cancellationToken) =>
                                await _manageReviewService.UpdateReview(request);
    }
}
