using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.Category;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;


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
    }
}
