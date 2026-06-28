using MediatR;
using ReelRating.Core.Schema.AuthSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.AuthSchema.Request
{
    public class SignInRequest : IRequest<Result<AuthUserResponse>>
    {
        public string? User { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}
