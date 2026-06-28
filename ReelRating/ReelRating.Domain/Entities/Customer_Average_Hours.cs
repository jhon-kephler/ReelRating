using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Customer_Average_Hours
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int? Hours { get; set; }
        public string? Field { get; set; }
    }
}
