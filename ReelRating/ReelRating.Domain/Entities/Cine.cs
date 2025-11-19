using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ReelRating.Domain.Entities
{
    public class Cine
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private int Year { get; set; }
        private int Month { get; set; }
        private int Whatch_Id { get; set; }
        private int Type_Id { get; set; }
        private string URL_Poster { get; set; }
    }
}
