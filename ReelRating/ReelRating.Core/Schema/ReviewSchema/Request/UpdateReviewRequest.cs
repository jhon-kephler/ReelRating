using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.ReviewSchema.Request
{
    public class UpdateReviewRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public string? REVIEW { get; set; }
        public int? Note { get; set; }
    }
}
