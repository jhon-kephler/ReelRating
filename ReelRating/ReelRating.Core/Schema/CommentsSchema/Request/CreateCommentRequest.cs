using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.CommentsSchema.Request
{
    public class CreateCommentRequest : IRequest<Result>
    {
        public int CustomerId { get; set; }
        public int CineId { get; set; }
        public string? CommentText { get; set; }
    }
}
