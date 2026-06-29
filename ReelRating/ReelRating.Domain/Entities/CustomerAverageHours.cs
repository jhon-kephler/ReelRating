using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class CustomerAverageHours
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? Hours { get; set; }
        public string? Field { get; set; }
    }
}
