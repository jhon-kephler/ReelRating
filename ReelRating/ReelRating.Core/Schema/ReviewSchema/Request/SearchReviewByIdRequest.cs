using MediatR;
using ReelRating.Core.Schema.ReviewSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.ReviewSchema.Request
{
    public class SearchReviewByIdRequest : IRequest<Result<ReviewResponse>>
    {
        public int Id { get; set; }
    }
}
