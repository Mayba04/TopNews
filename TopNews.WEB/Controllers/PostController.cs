using Microsoft.AspNetCore.Mvc;
using TopNews.Core.Services;

namespace TopNews.WEB.Controllers
{
    public class PostController : Controller
    {
        private readonly UserService _userService;
        private readonly CategoryService _categoryService;


        public PostController(UserService userService, CategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
