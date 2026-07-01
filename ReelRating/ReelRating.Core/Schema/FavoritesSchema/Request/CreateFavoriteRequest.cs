using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.FavoritesSchema.Request
{
    public class CreateFavoriteRequest : IRequest<Result>
    {
        public int CustomerId { get; set; }
        public int CineId { get; set; }
    }
}
