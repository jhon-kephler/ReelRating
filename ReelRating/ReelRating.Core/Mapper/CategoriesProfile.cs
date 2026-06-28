using AutoMapper;
using ReelRating.Core.Schema.HomeSchema.Response;
using ReelRating.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Core.Mapper
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Categories, CategoriesResponse>().ReverseMap();
        }
    }
}
