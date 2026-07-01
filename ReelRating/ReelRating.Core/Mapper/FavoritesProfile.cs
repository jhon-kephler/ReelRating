using AutoMapper;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Core.Schema.FavoritesSchema.Response;
using ReelRating.Domain.Entities;

namespace ReelRating.Core.Mapper
{
    public class FavoritesProfile : Profile
    {
        public FavoritesProfile()
        {
            CreateMap<Favorites, FavoritesResponse>().ReverseMap();
            CreateMap<Favorites, CreateFavoriteRequest>();
        }
    }
}
