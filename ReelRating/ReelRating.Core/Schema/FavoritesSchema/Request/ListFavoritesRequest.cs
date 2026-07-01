using MediatR;
using ReelRating.Core.Schema.FavoritesSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.FavoritesSchema.Request
{
    public class ListFavoritesRequest : IRequest<PaginationResult<FavoritesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
