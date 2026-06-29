using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.AuthSchema.Request
{
    public class CreateRequest : IRequest<Result<bool>>
    {
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
