using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Danhsachlinhkien;
using ViewModels.Lichsuthaotac.Request;
using ViewModels.System.User;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILichsuthaotacApiClient _lichsuthaotacApiClient;
        private readonly IUserApiClient _userApiClient;
        public UserController(IUserApiClient userApiClient, ILichsuthaotacApiClient lichsuthaotacApiClient)
        {
            _userApiClient = userApiClient;
            _lichsuthaotacApiClient = lichsuthaotacApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
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
                string hoten = HttpContext.Session.GetString("Token");
                var lichsuthaotac = new LichsuthaotacCreateRequest()
                {
                    Nguoi = hoten,
                    Loaithaotac = "CREATE",
                    Noidungthaotac = "Tạo tài khoản mới: " + request.UserName + " - " + request.Name,
                    Linhkienid = null,
                    Dathangid = null
                };
                await _lichsuthaotacApiClient.Create(lichsuthaotac);
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
                string hoten = HttpContext.Session.GetString("Token");
                var lichsuthaotac = new LichsuthaotacCreateRequest()
                {
                    Nguoi = hoten,
                    Loaithaotac = "UPDATE",
                    Noidungthaotac = "Cập nhật thông tin tài khoản: " + request.UserName + " - " + request.Name,
                    Linhkienid = null,
                    Dathangid = null
                };
                await _lichsuthaotacApiClient.Create(lichsuthaotac);
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
                return Json(new { success = false });
            }
            var result = await _userApiClient.EditRoleUser(id, request);
            if (result.IsSuccessed)
            {               
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _userApiClient.GetUserById(id);
            return View(result.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string usn, string hoten)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }
            var result = await _userApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                string tennguoithaotac = HttpContext.Session.GetString("Token");
                var lichsuthaotac = new LichsuthaotacCreateRequest()
                {
                    Nguoi = tennguoithaotac,
                    Loaithaotac = "DELETE",
                    Noidungthaotac = "Xoá tài khoản: " + usn + " - " + hoten,
                    Linhkienid = null,
                    Dathangid = null
                };
                await _lichsuthaotacApiClient.Create(lichsuthaotac);
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
