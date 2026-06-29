using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ReelRating.Domain.Entities
{
    public class Cine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? TmdbId { get; set; }
        public int? WhatchId { get; set; }
        public int? TypeId { get; set; }
        public string? URLPoster { get; set; }
        public ICollection<CineCategories> CineCategories { get; set; } = [];

    }
}
