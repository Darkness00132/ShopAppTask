using AutoMapper;
using Microsoft.EntityFrameworkCore;
using myshop.BLL.DTOs.Product;
using myshop.DAL.Repostiories;
using myshop.Entities.Models;

namespace myshop.BLL.Mangers
{
    public class ProductManager
    {
        private readonly IMapper _mapper;
        private readonly ProductRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public ProductManager(IMapper mapper, ProductRepository repository, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _repository.FindOneAsync(id);
        }

        public async Task<IEnumerable<ProductItem>> GetAllProductsWithCategoryNameAsync()
        {
            var products = await _repository.GetAllProductsWithCategoryAsync();
            return _mapper.Map<IEnumerable<ProductItem>>(products);
        }

        public async Task<Product> CreateProduct(CreateProduct dto, string RootPath)
        {
            var product = _mapper.Map<CreateProduct, Product>(dto);
            if (dto.File != null)
            {
                string filename = Guid.NewGuid().ToString();
                var Upload = Path.Combine(RootPath, @"Images\Products");
                var ext = Path.GetExtension(dto.File.FileName);

                using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                {
                    dto.File.CopyTo(filestream);
                }
                product.Img = @"Images\Products\" + filename + ext;
            }
            await _repository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return product;
        }

        public async Task UpdateProductAsync(int id , UpdateProduct dto, string RootPath) 
        {
            var product = await _repository.FindOneAsync(id);
            if (dto.File != null)
            {
                string filename = Guid.NewGuid().ToString();
                var Upload = Path.Combine(RootPath, @"Images\Products");
                var ext = Path.GetExtension(dto.File.FileName);

                if (product.Img != null)
                {
                    var oldimg = Path.Combine(RootPath, product.Img.TrimStart('\\'));

                    if (System.IO.File.Exists(oldimg))
                    {
                        System.IO.File.Delete(oldimg);
                    }
                }

                using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                {
                    dto.File.CopyTo(filestream);
                }

                product.Img = @"Images\Products\" + filename + ext;
            }
            _repository.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }

       public async Task<bool> DeleteProductAsync(int id,string RootPath)
        {
            var productIndb = await _repository.FindOneAsync(id);

            if (productIndb == null)
            {
                return false;
            }

            _repository.Remove(productIndb);

            var oldimg = Path.Combine(RootPath, productIndb.Img.TrimStart('\\'));

            if (System.IO.File.Exists(oldimg))
            {
                System.IO.File.Delete(oldimg);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        } 
    }
}
