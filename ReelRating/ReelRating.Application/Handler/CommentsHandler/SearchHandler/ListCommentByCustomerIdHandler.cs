using MediatR;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Handler.CommentsHandler.SearchHandler
{
    public class ListCommentByCustomerIdHandler : IRequestHandler<ListCommentsByCustomerIdRequest, PaginationResult<CommentResponse>>
    {
        private readonly ISearchCommentsService _searchCommentsService;

        public ListCommentByCustomerIdHandler(ISearchCommentsService searchCommentsService)
        {
            _searchCommentsService = searchCommentsService;
        }

        public async Task<PaginationResult<CommentResponse>> Handle(ListCommentsByCustomerIdRequest request, CancellationToken cancellationToken) =>
                                await _searchCommentsService.ListCommentsById(request);
    }
}
