using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.ReviewSchema.Request
{
    public class DeleteReviewRequest : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
