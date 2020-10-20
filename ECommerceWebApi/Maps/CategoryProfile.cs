using AutoMapper;
using ECommerceModels.DTOs;
using ECommerceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWebApi.Maps
{
    public class CategoryProfile : Profile
    {
        CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}
