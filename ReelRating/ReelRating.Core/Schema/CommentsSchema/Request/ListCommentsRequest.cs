using MediatR;
using ReelRating.Core.Schema.CommentsSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.CommentsSchema.Request
{
    public class ListCommentsRequest : IRequest<PaginationResult<CommentResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
