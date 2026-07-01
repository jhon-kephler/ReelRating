using Moq;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;

namespace ReelRating.Tests.Mock.CommentMock;

public static class IManageCommentServiceMock
{
    public static Mock<IManageCommentService> Create()
    {
        return new Mock<IManageCommentService>();
    }

    public static Mock<IManageCommentService> SetupCreateComment(this Mock<IManageCommentService> mock, CreateCommentRequest request, Result result)
    {
        mock.Setup(x => x.CreateComment(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageCommentService> SetupCreateCommentSuccess(this Mock<IManageCommentService> mock, CreateCommentRequest request)
    {
        var result = new Result();
        result.SetSuccess(true);
        mock.Setup(x => x.CreateComment(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageCommentService> SetupDeleteComment(this Mock<IManageCommentService> mock, DeleteCommentRequest request, Result result)
    {
        mock.Setup(x => x.DeleteComment(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<IManageCommentService> SetupDeleteCommentSuccess(this Mock<IManageCommentService> mock, DeleteCommentRequest request)
    {
        var result = new Result();
        result.SetSuccess(true);
        mock.Setup(x => x.DeleteComment(request)).ReturnsAsync(result);
        return mock;
    }
}
