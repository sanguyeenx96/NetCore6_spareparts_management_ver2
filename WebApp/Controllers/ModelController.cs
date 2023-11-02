using Microsoft.AspNetCore.Mvc;
using ViewModels.Lichsuthaotac.Request;
using ViewModels.Model;
using ViewModels.Model.Request;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class ModelController : BaseController
    {
        private readonly IModelApiClient _modelApiClient;
        private readonly ILichsuthaotacApiClient _lichsuthaotacApiClient;

        public ModelController(IModelApiClient modelApiClient, ILichsuthaotacApiClient lichsuthaotacApiClient)
        {
            _modelApiClient = modelApiClient;
            _lichsuthaotacApiClient = lichsuthaotacApiClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(ModelCreateRequest request)
        {
            var result = await _modelApiClient.Create(request);
            if (result.IsSuccessed)
            {
                string hoten = HttpContext.Session.GetString("Token");
                var lichsuthaotac = new LichsuthaotacCreateRequest()
                {
                    Nguoi = hoten,
                    Loaithaotac = "CREATE",
                    Noidungthaotac = "Tạo mới Model" + request.Tenmodel,
                    Linhkienid = null,
                    Dathangid = null
                };
                await _lichsuthaotacApiClient.Create(lichsuthaotac);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = result.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.currentPage = "Quản lý Model";
            var danhsachmodel = await _modelApiClient.GetAll();
            return View(danhsachmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id,string tenmodel)
        {
            var result = await _modelApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                string hoten = HttpContext.Session.GetString("Token");
                var lichsuthaotac = new LichsuthaotacCreateRequest()
                {
                    Nguoi = hoten,
                    Loaithaotac = "DELETE",
                    Noidungthaotac = "Xoá Model: "+tenmodel,
                    Linhkienid = null,
                    Dathangid = null
                };
                await _lichsuthaotacApiClient.Create(lichsuthaotac);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = result.Message });
            }
        }
    }
}
