using MediatR;
using ReelRating.Core.Schema.CommentsSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.CommentsSchema.Request
{
    public class SearchCommentByCustomerIdRequest : IRequest<Result<CommentResponse>>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

    }
}