using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.FavoritesSchema.Response
{
    public class FavoritesResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CineId { get; set; }
    }
}
