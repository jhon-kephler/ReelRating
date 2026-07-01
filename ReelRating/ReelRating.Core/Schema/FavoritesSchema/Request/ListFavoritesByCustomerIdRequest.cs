using MediatR;
using ReelRating.Core.Schema.FavoritesSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.FavoritesSchema.Request
{
    public class ListFavoritesByCustomerIdRequest : IRequest<PaginationResult<FavoritesResponse>>
    {
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
