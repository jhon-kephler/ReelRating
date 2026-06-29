using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.AuthSchema.Response
{
    public class AuthUserResponse
    {
        public UserInfo User { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
