using ReelRating.Core.Schema;
using ReelRating.Core.Schema.ReviewSchema.Request;

namespace ReelRating.Application.Services.ReviewServices.Interfaces
{
    public interface IManageReviewService
    {
        Task<Result> CreateReview(CreateReviewRequest request);
        Task<Result> UpdateReview(UpdateReviewRequest request);
        Task<Result> DeleteReview(DeleteReviewRequest request);
    }
}