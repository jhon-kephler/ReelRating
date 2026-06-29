using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class AverageHours
    {
        public int Id { get; set; }
        public int? HoursId { get; set; }
        public int? Hours { get; set; }
        public int? Mount { get; set; }
    }
}
