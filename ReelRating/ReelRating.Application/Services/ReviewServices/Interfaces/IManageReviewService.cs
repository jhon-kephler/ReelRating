using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;

namespace ReelRating.Application.Services.ReviewServices.Interfaces
{
    public interface IManageReviewService
    {
        Task<Result<bool>> CreateReview(CreateReviewRequest request);
        Task<Result<bool>> UpdateReview(UpdateReviewRequest request);
        Task<Result<bool>> DeleteReview(DeleteReviewRequest request);
    }
}