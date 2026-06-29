using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }
    }
}
