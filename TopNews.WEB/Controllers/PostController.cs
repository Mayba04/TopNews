using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.Post;

namespace TopNews.WEB.Controllers
{
    public class PostController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;


        public PostController(ICategoryService userService, IPostService postService)
        {
            _categoryService = userService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllPost()
        {
            var categoriesTask = await _postService.GetAll();

            return View(categoriesTask);
        }

        public async Task<IActionResult> AddPost()
        {
            await LoadCategory();
            return View();
        }

        private async Task LoadCategory()
        {
            List<CategoryDTO> result = await _categoryService.GetAll();
            @ViewBag.Category = new SelectList((System.Collections.IEnumerable)result,
                nameof(IdentityRole.Name), nameof(IdentityRole.Name)
              );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(PostDTO model)
        {
            var validator = new CreatePostValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var categoryModel = await _categoryService.GetByName(model.CategoryName);
                if (categoryModel != null)
                {
                    model.CategoryId = categoryModel.Id;
                    model.CategoryName = categoryModel.Name;
                    await _postService.Create(model);
                    return RedirectToAction(nameof(GetAllPost));
                }
                ViewBag.AuthError = "Such a category does not exist";
                return View();
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View();
        }


        public async Task<IActionResult> DeletePost(int id)
        {
            var model = await _postService.Get(id);

            if (model == null)
            {
                ViewBag.AuthError = " Post not found.";
                return View();
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _postService.Delete(Id);
            return RedirectToAction(nameof(GetAllPost));
        }

    }
}
