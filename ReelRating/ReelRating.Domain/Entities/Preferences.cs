using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Preferences
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int Categories_Id { get; set; }
        public string? Note_Origin { get; set; }
    }
}
