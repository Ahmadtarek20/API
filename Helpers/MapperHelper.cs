using System.Linq;
using System.IO;
using AppApi.Dto;
using AppApi.Models;
using AutoMapper;

namespace AppApi.Helpers
{
    public class MapperHelper : Profile
    {
        public MapperHelper()
        {
            CreateMap<Product , ProductDto>()
            .ForMember(dest =>dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault().Url);
            });
            CreateMap<Product , ProductsDetails>()
             .ForMember(dest =>dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault().Url);
            });
            CreateMap<Photo , PhotosDetails>();

        }
    }
}