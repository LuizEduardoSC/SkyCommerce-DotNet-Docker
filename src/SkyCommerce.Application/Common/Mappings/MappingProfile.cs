using AutoMapper;
using SkyCommerce.Application.DTOs;
using SkyCommerce.Domain.Entities;

namespace SkyCommerce.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Category, CategoryDto>();
    }
}
