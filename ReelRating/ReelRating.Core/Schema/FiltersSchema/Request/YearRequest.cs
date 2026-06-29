using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.FiltersSchema.Request
{
    public class YearRequest : IRequest<PaginationResult<int?>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } 
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}
