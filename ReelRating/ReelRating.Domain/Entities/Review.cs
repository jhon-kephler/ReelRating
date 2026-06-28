using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int Cine_Id { get; set; }
        public int? Categories_Id { get; set; }
        public int? Type_Id { get; set; }
        public int? Status_Id { get; set; }
        public string? REVIEW { get; set; }
        public int? Note { get; set; }
        public bool? Deleted { get; set; }
    }
}
