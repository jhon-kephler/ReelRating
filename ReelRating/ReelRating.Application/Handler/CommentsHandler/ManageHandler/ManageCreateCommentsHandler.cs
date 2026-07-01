using MediatR;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.AuthSchema.Request;
using ReelRating.Core.Schema.AuthSchema.Response;
using ReelRating.Core.Schema.CommentsSchema.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.CommentsHandler.ManageHandler
{
    public class ManageCreateCommentsHandler : IRequestHandler<CreateCommentRequest, Result>
    {
        private readonly IManageCommentService _manageCommentService;

        public ManageCreateCommentsHandler(IManageCommentService manageCommentService)
        {
            _manageCommentService = manageCommentService;
        }

        public async Task<Result> Handle(CreateCommentRequest request, CancellationToken cancellationToken) =>
                                await _manageCommentService.CreateComment(request);
    }
}
