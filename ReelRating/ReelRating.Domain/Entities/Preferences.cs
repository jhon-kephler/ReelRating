using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Domain.Entities
{
    public class Preferences
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CategoriesId { get; set; }
        public string? NoteOrigin { get; set; }
    }
}
