using AutoMapper;
using myshop.BLL.DTOs.Category;
using myshop.DAL.Repostiories;
using myshop.Entities.Models;

namespace myshop.BLL.Managers
{
    public class CategoryManager
    {
        private readonly IMapper _mapper;
        private readonly CategoryRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public CategoryManager(IMapper mapper, CategoryRepository repository, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _repository.FindOneAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddCategoryAsync(CreateCategory category)
        {
            var categoryEntity = _mapper.Map<CreateCategory, Category>(category);
            await _repository.AddAsync(categoryEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(UpdateCategory category)
        {
            var categoryEntity = _mapper.Map<UpdateCategory, Category>(category);
            _repository.Update(categoryEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repository.FindOneAsync(id);
            if (category != null)
            {
                _repository.Remove(category);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
