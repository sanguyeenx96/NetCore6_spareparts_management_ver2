using Microsoft.AspNetCore.Mvc;
using ViewModels.Model;
using ViewModels.Model.Request;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class ModelController : BaseController
    {
        private readonly IModelApiClient _modelApiClient;
        public ModelController(IModelApiClient modelApiClient)
        {
            _modelApiClient = modelApiClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(ModelCreateRequest request)
        {
            var result = await _modelApiClient.Create(request);
            if (result.IsSuccessed)
            {
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
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _modelApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = result.Message });
            }
        }
    }
}
