using MediatR;
using ReelRating.Core.Schema.CineSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Schema.CineSchema.Request
{
    public class CineRequest : IRequest<Result<CineResponse>>
    {
        public string Name { get; set; }
    }
}
