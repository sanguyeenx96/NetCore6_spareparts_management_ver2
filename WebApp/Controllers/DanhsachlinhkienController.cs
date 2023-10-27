﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using ViewModels.Count;
using ViewModels.Danhsachlinhkien;
using ViewModels.Dathang;
using ViewModels.System.User;
using WebApp.Services;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
namespace WebApp.Controllers
{
    public class DanhsachlinhkienController : BaseController
    {
        private readonly IDanhsachlinhkienApiClient _danhsachlinhkienApiClient;
        private readonly IModelApiClient _modelApiClient;
        private readonly IDathangApiClient _dathangApiClient;

        public DanhsachlinhkienController(IDanhsachlinhkienApiClient danhsachlinhkienApiClient, IModelApiClient modelApiClient, IDathangApiClient dathangApiClient)
        {
            _danhsachlinhkienApiClient = danhsachlinhkienApiClient;
            _modelApiClient = modelApiClient;
            _dathangApiClient = dathangApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Timkiemnhanh(string keyword, string? model, int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.currentPage = "Tìm kiếm linh kiện";
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

        [HttpGet]
        public async Task<IActionResult> Quanly()
        {
            ViewBag.currentPage = "Quản lý linh kiện";
            ViewBag.listmodel = await _modelApiClient.GetAll();
            var models = await _modelApiClient.GetAll();
            ViewBag.models = models.Select(x => new SelectListItem()
            {
                Text = x.Tenmodel,
                Value = x.Tenmodel
            });
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDanhsachlinhkien(string? model)
        {
            if (model == "All")
            {
                model = null;
            }          
            ViewBag.currentPage = "Quản lý linh kiện";
            var request = new GetDanhsachlinhkienRequest()
            {
                Model = model
            };
            var result = await _danhsachlinhkienApiClient.GetAll(request);
            return PartialView("_DanhsachlinhkienQuanly", result.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountsLinhkien(string model)
        {
            if (model == "All")
            {
                model = null;
            }
            var result = new CountVm()
            {
                dulieu1 = await GetCountDanhsachlinhkien(model, ""),
                dulieu2 = await GetCountDanhsachlinhkien(model, "0"),
                dulieu3 = await GetCountDathang(model, "Yêu cầu đặt hàng"),
                dulieu4 = await GetCountDathang(model, "Đang đặt hàng")
            };
            return PartialView("_InfoboxLinhkien", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountsDathang(string model)
        {
            if (model == "All")
            {
                model = null;
            }
            var result = new CountVm()
            {
                dulieu1 = await GetCountDathang(model, "Yêu cầu đặt hàng"),
                dulieu2 = await GetCountDathang(model, "Đang đặt hàng"),
                dulieu3 = await GetCountDathang(model, "Hàng về thiếu"),
            };
            return PartialView("_InfoboxDathang", result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _danhsachlinhkienApiClient.GetById(id);
            return View(data.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
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

        [HttpGet]
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

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImportExcel([FromForm] IFormFile file, string model)
        {
            using (var fileStream = file.OpenReadStream())
            {
                var result = await _danhsachlinhkienApiClient.ImportExcel(fileStream, model);
                if (result.IsSuccessed)
                {
                    return Json(new { success = true, data = result.ResultObj });
                }
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<int> GetCountDanhsachlinhkien(string? model, string? keyword)
        {
            var request = new GetDanhsachlinhkienRequest()
            {
                Model = model,
                Keyword = keyword
            };
            var result = await _danhsachlinhkienApiClient.Count(request);
            return result;
        }

        [HttpGet]
        public async Task<int> GetCountDathang(string? model, string? trangthai)
        {
            if (model == "All")
            {
                model = null;
            }
            var request = new GetDathangRequest()
            {
                Model = model,
                Trangthai = trangthai
            };
            var result = await _dathangApiClient.Count(request);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Laylinhkien(int id,LaylinhkienRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }
            var result = await _danhsachlinhkienApiClient.Laylinhkien(id, request);
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
