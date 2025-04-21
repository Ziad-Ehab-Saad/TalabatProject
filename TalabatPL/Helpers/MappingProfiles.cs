using AutoMapper;
using TalabatCore.Entities;
using TalabatPL.DTOs;

namespace TalabatPL.Helpers
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.Brand, options => options.MapFrom(s => s.Brand.Name))
                .ForMember(dest => dest.Category, options => options.MapFrom(s => s.Category.Name))
               .ForMember(dest => dest.PictureUrl, O => O.MapFrom<ProductPictureResolver>());


        }
    }
}
