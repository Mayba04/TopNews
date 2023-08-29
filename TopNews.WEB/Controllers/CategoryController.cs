using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.Category;
using TopNews.Core.Validation.User;
using X.PagedList;

namespace TopNews.WEB.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly UserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;

        public CategoryController(UserService userService, ICategoryService categoryService, IPostService postService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _postService = postService;
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
            var categoryDto = await _categoryService.Get(id);

            if (categoryDto == null)
            {
                ViewBag.AuthError = "Category not found.";
                return RedirectToAction(nameof(GetAllCategory));
            }

            List<PostDTO> posts = await _postService.GetByCategory(id);
            ViewBag.CategoryName = categoryDto.Name;
            ViewBag.CategoryId = categoryDto.Id;

            int pageSize = 20;
            int pageNumber = 1;
            return View("DeleteCategory", posts.ToPagedList(pageNumber, pageSize));
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
            ServiceResponse result = await _categoryService.GetByName(model);
            if (!result.Success)
            {
                ViewBag.AuthError = "Category exists.";
                return View(model);
            }
            var validator = new CreateCategoryValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _categoryService.Update(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);


            //var validator = new CreateCategoryValidation();
            //var validationResult = await validator.ValidateAsync(model);
            //if (validationResult.IsValid)
            //{
            //    await _categoryService.Update(model);
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewBag.AuthError = validationResult.Errors[0];
            //return View(model);


        }
    }
}
