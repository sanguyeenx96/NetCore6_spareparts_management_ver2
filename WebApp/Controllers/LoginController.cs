using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.System.User;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        public LoginController(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authen(LoginRequest request)
        {
            var result = await _userApiClient.Authenticate(request);
            if (result.IsSuccessed)
            {
                if (result.ResultObj == null)
                {
                    return Json(new { success = false, message = result.Message });
                }
                else
                {
                    TempData["result"] = "success";
                    HttpContext.Session.SetString("Token", result.ResultObj.Hoten);
                    return Json(new { success = true });
                }
            }
            else
            {
                return Json(new { success = false, message = result.Message });
            }                     
        }
    }
}
