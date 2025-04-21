using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TalabatCore.Entities;
using TalabatPL.DTOs;

namespace TalabatPL.Helpers
{
    public class ProductPictureResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}    