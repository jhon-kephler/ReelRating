using MediatR;
using ReelRating.Core.Schema.CineSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.CineSchema.Request
{
    public class ListCineDefaultRequest : IRequest<PaginationResult<CineResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
