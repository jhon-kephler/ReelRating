using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Data.Command.CommentCommand;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.CommentServices.Interfaces
{
    public interface IManageCommentService
    {
        Task<Result> CreateComment(CreateCommentRequest request);
        Task<Result> DeleteComment(DeleteCommentRequest request);
    }
}
