using AutoMapper;
using ECommerceModels.DTOs;
using ECommerceModels.Models;

namespace ECommerceWebApi.Maps
{
    public class OptionProfile : Profile
    {
        public OptionProfile()
        {
            CreateMap<OptionDTO, Option>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


            CreateMap<Option, OptionDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));            
        }
    }
}
