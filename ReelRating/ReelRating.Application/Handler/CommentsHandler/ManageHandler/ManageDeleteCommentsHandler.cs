using MediatR;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.CommentsHandler.ManageHandler
{
    public class ManageDeleteCommentsHandler : IRequestHandler<DeleteCommentRequest, Result>
    {
        private readonly IManageCommentService _manageCommentService;

        public ManageDeleteCommentsHandler(IManageCommentService manageCommentService)
        {
            _manageCommentService = manageCommentService;
        }

        public async Task<Result> Handle(DeleteCommentRequest request, CancellationToken cancellationToken) =>
                                await _manageCommentService.DeleteComment(request);
    }
}
