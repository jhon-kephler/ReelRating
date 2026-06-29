using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.HomeSchema.Response
{
    public class CategoriesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Type_Id { get; set; }
    }
}
