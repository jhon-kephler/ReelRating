using Moq;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;

namespace ReelRating.Tests.Mock.CommentMock;

public static class ISearchCommentsServiceMock
{
    public static Mock<ISearchCommentsService> Create()
    {
        return new Mock<ISearchCommentsService>();
    }

    public static Mock<ISearchCommentsService> SetupSearchCommentById(this Mock<ISearchCommentsService> mock, SearchCommentsByCustomerIdRequest request, Result<CommentResponse> result)
    {
        mock.Setup(x => x.SearchCommentById(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<ISearchCommentsService> SetupListComments(this Mock<ISearchCommentsService> mock, ListCommentsRequest request, PaginationResult<CommentResponse> result)
    {
        mock.Setup(x => x.ListComments(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<ISearchCommentsService> SetupListCommentsById(this Mock<ISearchCommentsService> mock, ListCommentsByCustomerIdRequest request, PaginationResult<CommentResponse> result)
    {
        mock.Setup(x => x.ListCommentsById(request)).ReturnsAsync(result);
        return mock;
    }
}
