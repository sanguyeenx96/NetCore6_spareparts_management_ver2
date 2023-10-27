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
            ViewBag.currentPage = "Quản lý đặt hàng";
            ViewBag.models = await _modelApiClient.GetAll();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDanhsachDathang(string? model, string trangthai)
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
            ViewBag.trangthai = trangthai;
            return PartialView("_Danhsachdathang", result.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateYeuCauDatHang(int linhkienid, string nguoithaotac, int soluong)
        {
            var request = new DathangCreateRequest()
            {
              Nguoithaotac = nguoithaotac,
              Soluong = soluong                
            };
            var result = await _dathangApiClient.CreateYeuCauDatHang(linhkienid, request);
            if (result.IsSuccessed)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
