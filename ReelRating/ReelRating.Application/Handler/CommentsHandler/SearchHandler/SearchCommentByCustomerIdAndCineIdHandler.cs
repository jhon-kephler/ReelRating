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
    public class SearchCommentByCustomerIdAndCineIdHandler : IRequestHandler<SearchCommentByCustomerIdAndCineIdRequest, Result<CommentResponse>>
    {
        private readonly ISearchCommentsService _searchCommentsService;

        public SearchCommentByCustomerIdAndCineIdHandler(ISearchCommentsService searchCommentsService)
        {
            _searchCommentsService = searchCommentsService;
        }

        public async Task<Result<CommentResponse>> Handle(SearchCommentByCustomerIdAndCineIdRequest request, CancellationToken cancellationToken) =>
                                await _searchCommentsService.SearchCommentByIdAndCineId(request);
    }
}
