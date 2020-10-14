using AutoMapper;
using ECommerceModels.DTOs;
using ECommerceModels.Models;

namespace ECommerceWebApi.Maps
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>();

            CreateMap<AddressDTO, Address>();
        }
    }
}
