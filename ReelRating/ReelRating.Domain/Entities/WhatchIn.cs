using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class WhatchIn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Region { get; set; }
        public bool Available { get; set; }
    }
}
