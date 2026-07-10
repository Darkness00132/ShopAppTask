using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myshop.BLL.DTOs.Product;
using myshop.BLL.Managers;
using myshop.BLL.Mangers;
using myshop.Entities.Models;
using myshop.Entities.ViewModels;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ProductController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ProductManager productManager, CategoryManager categoryManager, IWebHostEnvironment webHostEnvironment)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            var products = _productManager.GetAllProductsWithCategoryNameAsync();

            return Json(new { data = products });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryManager.GetAllCategoriesAsync();
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM, IFormFile file)
        {
            Product product = new Product();
            if (ModelState.IsValid)
            {
                var dto = new CreateProduct
                {
                    Name = productVM.Product.Name,
                    Description = productVM.Product.Description,
                    Price = productVM.Product.Price,
                    CategoryId = productVM.Product.CategoryId,
                    File = file
                };
                string RootPath = _webHostEnvironment.WebRootPath;
                product = await _productManager.CreateProduct(dto, RootPath);
                TempData["Create"] = "Item has Created Successfully";
                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var categories = await _categoryManager.GetAllCategoriesAsync();
            var product = await _productManager.GetProductAsync(id);

            ProductVM productVM = new ProductVM()
            {
                Product = product,
                CategoryList = categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                await _productManager.UpdateProductAsync(productVM.Product.Id,
                    new UpdateProduct()
                    {
                        Name = productVM.Product.Name,
                        Description = productVM.Product.Description,
                        Price = productVM.Product.Price,
                        CategoryId = productVM.Product.CategoryId,
                        File = file
                    }
                    , RootPath);

                TempData["Update"] = "Data has Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(productVM.Product);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productManager.DeleteProductAsync(id, _webHostEnvironment.WebRootPath);
            if (!result)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            return Json(new { success = true, message = "file has been Deleted" });
        }


    }
}
