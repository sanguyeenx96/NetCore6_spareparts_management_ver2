using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class LichsuController : BaseController
    {
        private readonly IDanhsachlinhkienApiClient _danhsachlinhkienApiClient;
        private readonly IModelApiClient _modelApiClient;
        private readonly IDathangApiClient _dathangApiClient;

        public LichsuController(IDanhsachlinhkienApiClient danhsachlinhkienApiClient, IModelApiClient modelApiClient, IDathangApiClient dathangApiClient)
        {
            _danhsachlinhkienApiClient = danhsachlinhkienApiClient;
            _modelApiClient = modelApiClient;
            _dathangApiClient = dathangApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
