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
                return RedirectToAction(nameof(GetAllCategory));
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View();
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.Get(id);
            if (result != null)
            {
                return View(result);
            }
            ViewBag.AuthError = "An error occurred";
            return RedirectToAction(nameof(GetAllCategory));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _categoryService.Delete(Id);
            return RedirectToAction(nameof(GetAllCategory));
        }

        public async Task<IActionResult> UpdateCategory(int Id)
        {
            var result = await _categoryService.Get(Id);
            if (result != null)
            {
                return View(result);
            }
            ViewBag.AuthError = "An error occurred";
            return RedirectToAction(nameof(GetAllCategory));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(CategoryDTO model)
        {
            var validator = new CreateCategoryValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var categoriesTask = _categoryService.GetAll();
                List<CategoryDTO> categories = await categoriesTask;
                bool containsCategory = categories.Any(cat => cat.Name == model.Name && cat.Id != model.Id);
                if (containsCategory)
                {
                    ViewBag.AuthError = "Such a category already exists";
                    return View();
                }
                await _categoryService.Update(model);
                return RedirectToAction(nameof(GetAllCategory));
            }
            ViewBag.AuthError = validationResult.Errors[0];
            //return View(model.Id);
            //await _categoryService.Update(model);
            //return RedirectToAction(nameof(GetAllCategory));
            return RedirectToAction(nameof(UpdateCategory),model.Id);

        }
    }
}
