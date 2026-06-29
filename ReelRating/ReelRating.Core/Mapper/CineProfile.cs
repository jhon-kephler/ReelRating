using AutoMapper;
using ReelRating.Core.Schema.CineSchema.Response;
using ReelRating.Core.Schema.HomeSchema.Response;
using ReelRating.Domain.Entities;

namespace ReelRating.Core.Mapper
{
    public class CineProfile : Profile
    {
        public CineProfile()
        {
            CreateMap<Cine, CineResponse>();
            CreateMap<Cine, CategoriesResponse>();
        }
    }
}
