using AutoMapper;
using ECommerceModels.DTOs;
using ECommerceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWebApi.Maps
{
    public class OrderProfile : Profile
    {
        OrderProfile()
        {
            CreateMap<Order, OrderDTO>();

            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
                
                
        }
    }
}
