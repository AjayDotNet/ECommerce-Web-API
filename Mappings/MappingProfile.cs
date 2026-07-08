using AutoMapper;
using ECommerceApi.DTOs;
using ECommerceApi.Models;

namespace ECommerceApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<CreateCategoryDto, Category>();


            // Product
            CreateMap<Product, ProductDto>()
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name)
                );

            CreateMap<CreateProductDto, Product>();

            CreateMap<UpdateProductDto, Product>();
        }
    }
}