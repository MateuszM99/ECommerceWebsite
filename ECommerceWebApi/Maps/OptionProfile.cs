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
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.OptionGroupId, opt => opt.MapFrom(src => src.OptionGroupId))
                .AfterMap((src, dest) =>
                {
                    dest.OptionGroup = new OptionGroup
                    {
                        Id = src.OptionGroupId,
                        Name = src.OptionGroupName
                    };

                });

            CreateMap<Option, OptionDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.OptionGroupId, opt => opt.MapFrom(src => src.OptionGroupId))
                .ForMember(dest => dest.OptionGroupName, opt => opt.MapFrom(src => src.OptionGroup.Name));
        }
    }
}
