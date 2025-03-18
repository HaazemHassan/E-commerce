using E_commerce.Models.Models;
using E_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_commerce.Models.DTO;

namespace E_commerce.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Products
            CreateMap<ProductAddRequest, Product>();
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<ProductUpdateRequest, Product>().ReverseMap();
            CreateMap<ProductResponse, ProductUpdateRequest>().ReverseMap();

            //Categories
            CreateMap<CategoryAddRequest, Category>();
            CreateMap<CategoryUpdateRequest, Category>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryResponse, CategoryUpdateRequest>();
        }
    }
}
