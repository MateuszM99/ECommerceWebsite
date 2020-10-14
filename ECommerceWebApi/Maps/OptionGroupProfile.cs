using AutoMapper;
using ECommerceModels.DTOs;
using ECommerceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWebApi.Maps
{
    public class OptionGroupProfile : Profile
    {
        OptionGroupProfile()
        {
            CreateMap<OptionGroup, OptionGroupDTO>();

            CreateMap<OptionGroupDTO, OptionGroup>();
        }
    }
}
