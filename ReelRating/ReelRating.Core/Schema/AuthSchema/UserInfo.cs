using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.AuthSchema
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; }
    }
}
