using AutoMapper;
using ECommerceModels.Models;
using ECommerceModels.RequestModels.ProductRequestModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWebApi.Maps.RequestModelsMaps
{
    public class CreateProductModelProfile : Profile
    {
       private NumberFormatInfo provider = new NumberFormatInfo();

        public CreateProductModelProfile()
        {
            this.provider.NumberDecimalSeparator = ",";

            CreateMap<CreateProductModel, Product>()
                .ForMember(dest => dest.Id,opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDouble(src.Price, this.provider)));              
                
                
        }
    }
}
