using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ReelRating.Domain.Entities
{
    public class Cine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Whatch_Id { get; set; }
        public int? Type_Id { get; set; }
        public string? URL_Poster { get; set; }
    }
}
