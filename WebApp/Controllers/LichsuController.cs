using Microsoft.AspNetCore.Mvc;
using ViewModels.Lichsuthaotac.Request;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class LichsuController : BaseController
    {
        private readonly IDanhsachlinhkienApiClient _danhsachlinhkienApiClient;
        private readonly IModelApiClient _modelApiClient;
        private readonly IDathangApiClient _dathangApiClient;
        private readonly ILichsuthaotacApiClient _lichsuthaotacApiClient;


        public LichsuController(IDanhsachlinhkienApiClient danhsachlinhkienApiClient, IModelApiClient modelApiClient, IDathangApiClient dathangApiClient, ILichsuthaotacApiClient lichsuthaotacApiClient)
        {
            _danhsachlinhkienApiClient = danhsachlinhkienApiClient;
            _modelApiClient = modelApiClient;
            _dathangApiClient = dathangApiClient;
            _lichsuthaotacApiClient = lichsuthaotacApiClient;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.currentPage = "Lịch sử hoạt động";
            ViewBag.models = await _modelApiClient.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetLichSu([FromBody]GetLichsuthaotacRequest request)
        {
            var result = await _lichsuthaotacApiClient.GetLichSu(request);
            return PartialView("_Lichsuthaotac", result.ResultObj);
        }
    }
}
