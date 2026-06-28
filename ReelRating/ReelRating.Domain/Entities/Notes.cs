using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Notes
    {
        public int Id { get; set; }
        public int Cine_Id { get; set; }
        public string? IMDB_Note { get; set; }
        public string? TOMMATERS_Note { get; set; }
        public string? POPCORNMETER { get; set; }
        public string? Customer_Notes { get; set; }
    }
}
