using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.CommentsSchema.Response
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CineId { get; set; }
        public string? CommentText { get; set; }
        public bool? Deleted { get; set; }
    }
}
