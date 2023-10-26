using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Danhsachlinhkien;
using ViewModels.System.User;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        public UserController(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.currentPage = "Quản lý tài khoản";
            var sessions = HttpContext.Session.GetString("Token");
            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUsersPaging(request);
            ViewBag.Keyword = keyword;
            return View(data.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                return Json(new { success = false, errors = errors });
            }
            var result = await _userApiClient.RegisterUser(request);
            if (result.IsSuccessed)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, errors = new List<string> { result.Message } });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                return Json(new { success = false, errors = errors });
            }
            var result = await _userApiClient.UpdateUser(id, request);
            if (result.IsSuccessed)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, errors = new List<string> { result.Message } });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(int id, UserEditRoleRequest request)
        {
            if (!ModelState.IsValid)
            {             
                return Json(new { success = false});
            }
            var result = await _userApiClient.EditRoleUser(id, request);
            if (result.IsSuccessed)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false});
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _userApiClient.GetUserById(id);
            return View(result.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }
            var result = await _userApiClient.Delete(request.Id);
            if (result.IsSuccessed)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("index", "Login");
        }

    }
}
