using ECommerceModels.DTOs;
using ECommerceModels.Models;
using AutoMapper;
using System.Linq;

namespace ECommerceWebApi.Maps
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.ProductOptions.Select(y => y.Option).ToList()))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.ProductOptions, opt => opt.MapFrom(src => src.Options))               
                .AfterMap((src, dest) =>
                {                                           
                    foreach (var option in dest.ProductOptions)
                    {
                        option.ProductId = (int)src.Id;
                    }                   
                })
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));  
                       
            CreateMap<OptionDTO, ProductOption>()
                .ForMember(dest => dest.OptionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Option, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.ProductStock, opt => opt.MapFrom(src => src.Stock));

            
                            
        }
    }
}
