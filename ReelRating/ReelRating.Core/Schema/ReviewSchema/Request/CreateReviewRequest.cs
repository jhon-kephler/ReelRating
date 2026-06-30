using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.ReviewSchema.Request
{
    public class CreateReviewRequest : IRequest<Result<bool>>
    {
        public int CustomerId { get; set; }
        public int CineId { get; set; }
        public int TypeId { get; set; }
        public string? REVIEW { get; set; }
        public int? Note { get; set; }
    }
}
