using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.CommentServices.Interfaces
{
    public interface ISearchCommentsService
    {
        Task<Result<CommentResponse>> SearchCommentById(SearchCommentsByCustomerIdRequest request);
        Task<Result<CommentResponse>> SearchCommentByIdAndCineId(SearchCommentByCustomerIdAndCineIdRequest request);
        Task<PaginationResult<CommentResponse>> ListComments(ListCommentsRequest request);
        Task<PaginationResult<CommentResponse>> ListCommentsById(ListCommentsByCustomerIdRequest request);
    }
}
