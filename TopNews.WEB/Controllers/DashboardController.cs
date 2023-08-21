using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopNews.Core.DTOs.Login;
using TopNews.Core.DTOs.User;
using TopNews.Core.Services;
using TopNews.Core.Validation.User;
using TopNews.WEB.Models.ViewModels;

namespace TopNews.WEB.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        private readonly UserService _userService;
        public DashboardController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]//GET
        public IActionResult Login()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
     
        [AllowAnonymous]//POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var validator = new LoginUserValidation();
            var validationResult = validator.Validate(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _userService.LoginUserAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.AuthError = result.Message;
                return View(model);
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>  Logout()
        {
            ServiceResponse result = await _userService.SignOutAsync();
            if (result.Success == true) 
            {
                return RedirectToAction(nameof(Login));
            }
            return View();

            //await HttpContext.SignOutAsync();


            //foreach (var cookie in Request.Cookies.Keys)
            //{
            //    Response.Cookies.Delete(cookie);
            //}

        }

        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return View(result.Payload);
        }

        public async Task<IActionResult> Profile(string Id)
        {
            var result = await _userService.GetUserByIdAsync(Id);
            if (result.Success)
            {
                UpdateProfileVM profile = new UpdateProfileVM()
                {
                    UserInfo = (UpdateUserDTO)result.Payload
                };
                return View(profile);
            }
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Profile(UpdatePasswordDTO model)
        {

            var validator = new UpdatePasswordValidation();// model.GetType();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.UpdatePasswordAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Login));
                }

                ViewBag.UpdatePasswordError = result.Payload;
                return View();

            }
            ViewBag.UpdatePasswordError = validationResult.Errors[0];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ChangeProfile(UpdateUserDTO model)
        {
            var validator = new UpdateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _userService.ChangeMainInfoUserAsync(model);
                if (result.Success)
                {
                    return View(nameof(Profile), new UpdateProfileVM() { UserInfo = model });
                }
                ViewBag.UserUpdateError = result.Payload;
                return View(nameof(Profile), new UpdateProfileVM() { UserInfo = model });
            }
            ViewBag.UserUpdateError = validationResult.Errors[0];
            return View(nameof(Profile), new UpdateProfileVM() { UserInfo = model });
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDTO model)
        {
            var validator = new CreateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.CreateUserAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(GetAll));
                }

                ViewBag.AuthError = result.Payload;
                return View();
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View();
        }


        public async Task<IActionResult> Delete(string id)
        {
            var res = await _userService.GetUserByIdAsync(id);
            return View(res.Payload);
        }

        public async Task<IActionResult> DeleteById(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.Success)
            {
                return View(nameof(Index));
            }
            ViewBag.AuthError = result.Errors;
            return View(nameof(Delete));
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.Success) 
            {
                return Redirect(nameof(SignIn));
            }
            return Redirect(nameof(SignIn));
        }


        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _userService.ForgotPasswordAsync(email);
            if (result.Success)
            {
                ViewBag.AuthError = result.Message;
                return View(nameof(Login));
            }
            ViewBag.AuthError = result.Message;
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            ViewBag.Email = email;
            ViewBag.Token = token;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var validator = new ResetPasswordValidation();
            var validationresult = await validator.ValidateAsync(model);
            if (validationresult.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);
                if (result.Success)
                {
                    ViewBag.AuthError = result.Message;
                    return View(nameof(Login));
                }
                ViewBag.AuthError = result.Message;
            }
            ViewBag.AuthError = validationresult.Errors[0];
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            await LoadRoles();
            var result = await _userService.GetUserByIdAsync(id);
            if (result.Success)
            {
                return View(result.Payload);
            }
            return View();
        }

        private async Task LoadRoles()
        {
            var result = await _userService.GetAllRolesAsync();
            ViewBag.RolesList = result;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditUser(UpdateUserDTO model)
        {
            var validator = new UpdateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                ServiceResponse result = await _userService.EditUserAsync(model);
                if (result.Success)
                {
                    return View(nameof(Index));
                }
                return View(nameof(Index));
            }
            await LoadRoles();
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(nameof(EditUser));
        }
    }
}
