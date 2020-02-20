using AutoMapper;
using testeItLab.domain.Models;
using testeItLab.domain.ViewModels;

namespace testeItLab.web.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.TargetGender, src => src.MapFrom(m => m.Sex));

            CreateMap<ProductViewModel, Product>()
                .ForMember(dest => dest.Sex, src => src.MapFrom(m => m.TargetGender));
        }
    }
}
