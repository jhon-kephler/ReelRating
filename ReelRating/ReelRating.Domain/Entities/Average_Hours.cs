using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Average_Hours
    {
        public int Id { get; set; }
        public int? Hours_Id { get; set; }
        public int? Hours { get; set; }
        public int? Mount { get; set; }
    }
}
