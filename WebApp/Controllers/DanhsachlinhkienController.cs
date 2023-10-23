using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using ViewModels.Danhsachlinhkien;
using WebApp.Services;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace WebApp.Controllers
{
    public class DanhsachlinhkienController : BaseController
    {
        private readonly IDanhsachlinhkienApiClient _danhsachlinhkienApiClient;
        private readonly IModelApiClient _modelApiClient;

        public DanhsachlinhkienController(IDanhsachlinhkienApiClient danhsachlinhkienApiClient, IModelApiClient modelApiClient)
        {
            _danhsachlinhkienApiClient = danhsachlinhkienApiClient;
            _modelApiClient = modelApiClient;
        }

        public async Task<IActionResult> Timkiemnhanh(string keyword, string? model, int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.currentPage = "Tìm kiếm";
            if (model == "All Models")
            {
                model = null;
            }
            var request = new GetDanhsachlinhkienPagingRequest()
            {
                Keyword = !string.IsNullOrEmpty(keyword) ? keyword.Trim().ToLower() : "",
                PageIndex = pageIndex,
                PageSize = pageSize,
                Model = model
            };
            var data = await _danhsachlinhkienApiClient.GetAllPaging(request);
            ViewBag.Keyword = !string.IsNullOrEmpty(keyword) ? keyword : null;

            var models = await _modelApiClient.GetAll();
            ViewBag.models = models.Select(x => new SelectListItem()
            {
                Text = x.Tenmodel,
                Value = x.Tenmodel,
                Selected = !string.IsNullOrEmpty(model) && model.Equals(x.Tenmodel)

            });
            return View(data.ResultObj);
        }

        public async Task<IActionResult> Quanly()
        {
            ViewBag.currentPage = "Spareparts List";
            var models = await _modelApiClient.GetAll();
            ViewBag.models = models.Select(x => new SelectListItem()
            {
                Text = x.Tenmodel,
                Value = x.Tenmodel
            });
            return View();
        }

        public async Task<IActionResult> GetDanhsachlinhkien(string? model)
        {
            if (model == "All Models")
            {
                model = null;
            }
            ViewBag.currentPage = "Spareparts List";
            var request = new GetDanhsachlinhkienRequest()
            {
                Model = model
            };
            var result = await _danhsachlinhkienApiClient.GetAll(request);
            return PartialView("_DanhsachlinhkienQuanly", result.ResultObj);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var data = await _danhsachlinhkienApiClient.GetById(id);
            return View(data.ResultObj);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] DanhsachlinhkienCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _danhsachlinhkienApiClient.Create(request);
            if (result.IsSuccessed)
            {
                return RedirectToAction("index");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        public async Task<IActionResult> ImportExcel()
        {
            ViewBag.currentPage = "Nhập dữ liệu linh kiện từ file Excel";
            var models = await _modelApiClient.GetAll();
            ViewBag.models = models.Select(x => new SelectListItem()
            {
                Text = x.Tenmodel,
                Value = x.Tenmodel
            });
            return View();
        }






    }
}
  