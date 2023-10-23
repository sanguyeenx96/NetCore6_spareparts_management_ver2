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
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.Authenticate(request);
            if(result.ResultObj == null)
            {
                ModelState.AddModelError("", result.Message);
                return View(); 
            }

            HttpContext.Session.SetString("Token", result.ResultObj.Hoten);

            TempData["result"] = "success";
            return RedirectToAction("Timkiemnhanh","Danhsachlinhkien");
        }

    }
}
