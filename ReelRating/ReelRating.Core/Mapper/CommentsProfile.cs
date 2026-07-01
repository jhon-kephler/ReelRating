using AutoMapper;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;
using ReelRating.Domain.Entities;

namespace ReelRating.Core.Mapper
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comments, CommentResponse>().ReverseMap();
            CreateMap<Comments, CreateCommentRequest>();
        }
    }
}
