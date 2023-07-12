using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.Login;
using TopNews.Core.Validation.User;

namespace TopNews.WEB.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]//GET
        public IActionResult Login()
        {
            return View();
        }
     
        [AllowAnonymous]//POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(LoginDTO model)
        {
            var validator = new LoginUserValidation();
            var validatotionResult = validator.Validate(model);
            if (validatotionResult.IsValid) 
            {
                return View();
            }
            ViewBag.AuthError = validatotionResult.Errors[0];
            return View(model);
            
        }

    }
}
