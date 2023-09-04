using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using TopNews.Core.DTOs.Post;
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

        public async Task<IActionResult> Index(int? id)
        {
            var categoriesTask = await _postService.GetAll(); ;
            int pageSize = 20;
            int pageNumber = id ?? 1;
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

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostsByCategory(int id)
        {
            List<PostDTO> posts = await _postService.GetByCategory(id);
            int pageSize = 20;
            int pageNumber = 1;
            return View("Index", posts.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([FromForm] string searchString)
        {
            List<PostDTO> posts = await _postService.Search(searchString);
            int pageSize = 20;
            int pageNumber = 1;
            return View("Index", posts.ToPagedList(pageNumber, pageSize));
        }

        public async Task<IActionResult> ShowPost(int id)
        {
            var model = await _postService.GetById(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}