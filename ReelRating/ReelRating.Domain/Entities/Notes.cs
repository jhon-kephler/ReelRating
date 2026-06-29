using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Notes
    {
        public int Id { get; set; }
        public int CineId { get; set; }
        public string? TMDBNote { get; set; }
        public string? CustomerNotes { get; set; }
    }
}
