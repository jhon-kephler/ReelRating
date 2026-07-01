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
    public class ListCommentsHandler : IRequestHandler<ListCommentsRequest, PaginationResult<CommentResponse>>
    {
        private readonly ISearchCommentsService _searchCommentsService;

        public ListCommentsHandler(ISearchCommentsService searchCommentsService)
        {
            _searchCommentsService = searchCommentsService;
        }

        public async Task<PaginationResult<CommentResponse>> Handle(ListCommentsRequest request, CancellationToken cancellationToken) =>
                                await _searchCommentsService.ListComments(request);
    
    }
}
