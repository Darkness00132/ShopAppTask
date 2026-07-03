using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Category;
using myshop.BLL.Managers;
using myshop.DataAccess;
using myshop.Entities.Models;

namespace myshop.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryManager _categoryManager;

        public CategoryController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryManager.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategory category)
        {
            if (ModelState.IsValid)
            {
                await _categoryManager.AddCategoryAsync(category);

                TempData["Create"] = "Item has Created Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var categoryIndb = await _categoryManager.GetCategoryByIdAsync(id);

            return View(categoryIndb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategory category)
        {
            if (ModelState.IsValid)
            {
                await _categoryManager.UpdateCategoryAsync(category);
                TempData["Update"] = "Data has Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var categoryIndb = await _categoryManager.GetCategoryByIdAsync(id);

            return View(categoryIndb);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryIndb = await _categoryManager.GetCategoryByIdAsync(id);
            if (categoryIndb == null)
            {
                return NotFound();
            }
            await _categoryManager.DeleteCategoryAsync(id);
            TempData["Delete"] = "Item has Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
