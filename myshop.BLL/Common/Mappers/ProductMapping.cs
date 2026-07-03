using AutoMapper;
using myshop.BLL.DTOs.Product;
using myshop.Entities.Models;

namespace myshop.BLL.Common.Mappers
{
    public class ProductMapping : Profile
    {
        public ProductMapping() 
        {
            CreateMap<Product, ProductItem>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CreateProduct , Product>();
            CreateMap<UpdateProduct, Product>()
                .ForAllMembers(opt => opt.Condition((src, dest) => src != null));
        }
    }
}
