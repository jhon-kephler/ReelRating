using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.CommentsSchema.Request
{
    public class DeleteCommentRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}