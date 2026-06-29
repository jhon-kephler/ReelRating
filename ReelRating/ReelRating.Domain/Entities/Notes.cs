using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Notes
    {
        public int Id { get; set; }
        public int CineId { get; set; }
        public string? IMDBNote { get; set; }
        public string? TOMMATERSNote { get; set; }
        public string? POPCORNMETER { get; set; }
        public string? Customer_Notes { get; set; }
    }
}
