using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Customer_Whatch
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int? Qtt_Access { get; set; }
        public int Whatch_Id { get; set; }
    }
}
