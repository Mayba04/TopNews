using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TopNews.Core.Interfaces;
using TopNews.WEB.Models;
using X.PagedList;

namespace TopNews.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;


        public HomeController(ICategoryService userService, IPostService postService)
        {
            _categoryService = userService;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var categoriesTask = await _postService.GetAll(); ;
            int pageSize = 20;
            int pageNumber = 1;
            categoriesTask.Reverse();
            return View("Index", categoriesTask.ToPagedList(pageNumber, pageSize));
        }

        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
             switch (statusCode) 
            {
                case 404:
                    return View("NotFound");
                default: return View("Error");
            }
        }
    }
}