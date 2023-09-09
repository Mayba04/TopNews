using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TopNews.Core.DTOs.Ip;
using TopNews.Core.DTOs.Post;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;
using TopNews.Core.Validation.Ip;
using TopNews.Core.Validation.Post;

namespace TopNews.WEB.Controllers
{
    public class IpController : Controller
    {
        private readonly IDashdoardAccessService _IpService;
        public IpController(IDashdoardAccessService dashdoardAccessService)
        {
            _IpService = dashdoardAccessService;
        }
        public IActionResult Index()
        {
            return RedirectToAction(nameof(GetAll));
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll()
        {
            List<DashboardAccessDTO> ips = await _IpService.GetAll();
            return View(ips);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DashboardAccessDTO model)
        {
            var validationResult = await new CreateIPValidation().ValidateAsync(model);
            if (validationResult.IsValid)
            {
                DashboardAccessDTO? result = await _IpService.Get(model.IpAddress);
                if (result != null)
                {
                    ViewBag.AuthError = "DashdoardAccesses exists.";
                    return View(model);
                }
                _IpService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AuthError = validationResult.Errors.FirstOrDefault();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var Ip = await _IpService.Get(id);

            if (Ip == null) return NotFound();

            return View(Ip);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DashboardAccessDTO model)
        {
            var validator = new CreateIPValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _IpService.Update(model);
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.CreatePostError = validationResult.Errors[0];
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            DashboardAccessDTO? model = await _IpService.Get(id);
            if (model == null)
            {
                ViewBag.AuthError = "Category not found.";
                return RedirectToAction(nameof(GetAll));
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteById(int Id)
        {
            await _IpService.Delete(Id);
            return RedirectToAction(nameof(GetAll));
        }

    }
}
