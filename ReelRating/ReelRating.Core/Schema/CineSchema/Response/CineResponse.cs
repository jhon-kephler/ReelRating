using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.CineSchema.Response
{
    public class CineResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? WhatchId { get; set; }
        public int? TypeId { get; set; }
        public string? URLPoster { get; set; }
    }
}
