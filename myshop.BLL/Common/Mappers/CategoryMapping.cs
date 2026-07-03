using AutoMapper;
using myshop.BLL.DTOs.Category;
using myshop.Entities.Models;

namespace myshop.BLL.Common.Mappers
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryItem>().ReverseMap();
            CreateMap<CreateCategory, Category>();
            CreateMap<UpdateCategory, Category>();
        }
    }
}
