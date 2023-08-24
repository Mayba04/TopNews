using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.Category;
using TopNews.Core.Services;
using TopNews.Core.Validation.Category;
using TopNews.Core.Validation.User;

namespace TopNews.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly UserService _userService;
        private readonly CategoryService _categoryService;

      
        public CategoryController(UserService userService, CategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
        }

        public IActionResult Index1()
        {
            return View();
        }

        public async Task<IActionResult> GetAllCategory()
        {
            var categoriesTask = await _categoryService.GetAll();

            return View(categoriesTask);

        }

        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryDTO model)
        {
            var validator = new CreateCategoryValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var categoriesTask = _categoryService.GetAll();
                List<CategoryDTO> categories = await categoriesTask;
                bool containsCategory = categories.Any(cat => cat.Name == model.Name);
                if (containsCategory) 
                {
                    ViewBag.AuthError = "Such a category already exists";
                    return View();
                }
                await _categoryService.Create(model);
                return View(nameof(GetAllCategory));
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View();
        }


    }
}
