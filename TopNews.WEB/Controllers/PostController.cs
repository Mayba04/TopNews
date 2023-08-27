using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.Category;
using TopNews.Core.Services;


namespace TopNews.WEB.Controllers
{
    public class PostController : Controller
    {
        private readonly UserService _userService;
        private readonly PostService _postService;


        public PostController(UserService userService, PostService postService)
        {
            _userService = userService;
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
