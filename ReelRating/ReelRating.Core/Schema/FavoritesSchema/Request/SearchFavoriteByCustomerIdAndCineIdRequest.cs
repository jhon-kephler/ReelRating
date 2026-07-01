using MediatR;
using ReelRating.Core.Schema.FavoritesSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.FavoritesSchema.Request
{
    public class SearchFavoriteByCustomerIdAndCineIdRequest : IRequest<Result<FavoritesResponse>>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CineId { get; set; }
    }
}
