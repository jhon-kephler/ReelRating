using MediatR;
using ReelRating.Core.Schema.CineSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.CineSchema.Request
{
    public class ListCineByFiltersRequest : IRequest<PaginationResult<CineResponse>>
    {
        public int? CategoriesId { get; set; }
        public int? Year { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
