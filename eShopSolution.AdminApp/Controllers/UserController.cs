using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        public UserController(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }
        public async Task<IActionResult> Index(string keyword = "",int pageIndex = 1, int pageSize = 2)
        {
            var request = new GetUserPagingRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                KeyWord = keyword
            };
            var data = await _userApiClient.GetUsersPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["SuccessMsg"] != null)
            {
                ViewBag.SuccessMsg = TempData["SuccessMsg"];
            }
            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid guid)
        {
            var result = await _userApiClient.GetById(guid);
            if (result.IsSuccessed)
            {
                return View(result.ResultObj);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid) // if login not success
            {
                return View();
            }
            var result = await _userApiClient.RegisterUser(request);
            if (result.IsSuccessed)
            {
                TempData["SuccessMsg"] = "Tạo người dùng thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid guid)
        {
            var result = await _userApiClient.GetById(guid);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateRequest()
                {
                    Dob = user.DoB,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = guid
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["SuccessMsg"] = "Cập nhật người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        public IActionResult Delete(Guid guid)
        {
            var user = new UserDeleteRequest()
            {
                Id = guid
            };
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.Delete(request.Id);
            if (result.IsSuccessed)
            {
                TempData["SuccessMsg"] = "Xóa người dùng thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }
    }
}
