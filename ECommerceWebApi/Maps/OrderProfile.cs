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
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.ClientSurname, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.ClientEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.ClientPhone, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items.Select(p => p.Product).ToList()));


            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
                
                
        }
    }
}
