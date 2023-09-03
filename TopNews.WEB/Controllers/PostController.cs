using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopNews.Core.DTOs.Category;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.Post;
using X.PagedList;

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
            var categoriesTask = await _postService.GetAll();;
            int pageSize = 20;
            int pageNumber = 1;
            //categoriesTask.Reverse();
            return View("GetAllPost", categoriesTask.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddPost()
        {
            await LoadCategory();
            return View();
        }

        private async Task LoadCategory()
        {
            ViewBag.CategoryList = new SelectList(
               await _categoryService.GetAll(),
               nameof(CategoryDTO.Id),
               nameof(CategoryDTO.Name)
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
                var files = HttpContext.Request.Form.Files;
                model.File = files;
                await _postService.Create(model);
                return RedirectToAction(nameof(GetAllPost));
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View();
        }

        [Authorize(Roles = "Administrator")]
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

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _postService.Delete(Id);
            return RedirectToAction(nameof(GetAllPost));
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var posts = await _postService.Get(id);

            if (posts == null) return NotFound();

            await LoadCategory();
            return View(posts);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostDTO model)
        {
            var validator = new CreatePostValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                model.File = files;
                await _postService.Update(model);
                return RedirectToAction(nameof(GetAllPost));
            }
            ViewBag.CreatePostError = validationResult.Errors[0];
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostsByCategory(int id)
        {
            List<PostDTO> posts = await _postService.GetByCategory(id);
            int pageSize = 20;
            int pageNumber = 1;
            return View("GetAllPost", posts.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([FromForm] string searchString)
        {
            List<PostDTO> posts = await _postService.Search(searchString);
            int pageSize = 20;
            int pageNumber = 1;
            return View("GetAllPost", posts.ToPagedList(pageNumber, pageSize));
        }

        public async Task<IActionResult> ShowPost(int id)
        {
            var model = await _postService.GetById(id);
            if (model == null)
            {
                return RedirectToAction(nameof(GetAllPost));
            }
            return View(model);
        }
        
    }
}
