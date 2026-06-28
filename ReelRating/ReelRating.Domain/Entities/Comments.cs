using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int Cine_Id { get; set; }
        public string? Comment_Text { get; set; }
        public bool? Deleted { get; set; }
    }
}
