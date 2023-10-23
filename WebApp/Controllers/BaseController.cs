using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var sessions = context.HttpContext.Session.GetString("Token");
            if (sessions == null)
            {
                TempData["info"] = "Bạn phải đăng nhập";
                context.Result = new RedirectToActionResult("index", "login",null);
            }

            base.OnActionExecuted(context);
        }

    }
}
