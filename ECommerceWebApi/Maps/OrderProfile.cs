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
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.User != null ? src.User.FirstName : src.ClientName))
                .ForMember(dest => dest.ClientSurname, opt => opt.MapFrom(src => src.User != null ? src.User.LastName : src.ClientSurname))
                .ForMember(dest => dest.ClientEmail, opt => opt.MapFrom(src => src.User != null ? src.User.Email : src.ClientEmail))
                .ForMember(dest => dest.ClientPhone, opt => opt.MapFrom(src => src.User != null ? src.User.PhoneNumber : src.ClientPhone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Select(p => p.Product).ToList()));


            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
                
                
        }
    }
}
