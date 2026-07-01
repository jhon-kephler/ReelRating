using MediatR;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;

namespace ReelRating.Application.Handler.CommentsHandler.SearchHandler
{
    public class SearchCommentByCustomerIdHandler : IRequestHandler<SearchCommentsByCustomerIdRequest, Result<CommentResponse>>
    {
        private readonly ISearchCommentsService _searchCommentsService;

        public SearchCommentByCustomerIdHandler(ISearchCommentsService searchCommentsService)
        {
            _searchCommentsService = searchCommentsService;
        }

        public async Task<Result<CommentResponse>> Handle(SearchCommentsByCustomerIdRequest request, CancellationToken cancellationToken) =>
                                await _searchCommentsService.SearchCommentById(request);
    }
}
