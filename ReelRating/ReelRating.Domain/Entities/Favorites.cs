using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Favorites
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CineId { get; set; }
        public bool? Deleted { get; set; }
    }
}
