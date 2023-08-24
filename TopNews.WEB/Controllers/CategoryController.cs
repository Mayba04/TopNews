using Microsoft.AspNetCore.Mvc;
using TopNews.Core.Services;

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
            var result = await _categoryService.GetAll();
            return View(result);
        }

    }
}
