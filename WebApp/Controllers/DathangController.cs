using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModels.Danhsachlinhkien;
using ViewModels.Dathang;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class DathangController : Controller
    {
        private readonly IDathangApiClient _dathangApiClient;
        private readonly IModelApiClient _modelApiClient;

        public DathangController(IDathangApiClient dathangApiClient, IModelApiClient modelApiClient)
        {
            _dathangApiClient = dathangApiClient;
            _modelApiClient = modelApiClient;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.currentPage = "Orders List";
            ViewBag.models = await _modelApiClient.GetAll();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDanhsachDathang(string? model, string? trangthai)
        {
            if (model == "All")
            {
                model = null;
            }
            var request = new GetDathangRequest()
            {
                Model = model,
                Trangthai = trangthai,
            };
            var result = await _dathangApiClient.GetAll(request);
            return PartialView("_Danhsachdathang", result.ResultObj);
        }
    }
}
