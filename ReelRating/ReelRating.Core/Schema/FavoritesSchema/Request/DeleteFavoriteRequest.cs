using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.FavoritesSchema.Request
{
    public class DeleteFavoriteRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
