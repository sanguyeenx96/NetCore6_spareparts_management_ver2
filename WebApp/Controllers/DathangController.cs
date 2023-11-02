using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModels.Danhsachlinhkien;
using ViewModels.Dathang;
using ViewModels.Dathang.Request;
using ViewModels.Lichsuthaotac.Request;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class DathangController : BaseController
    {
        private readonly ILichsuthaotacApiClient _lichsuthaotacApiClient;
        private readonly IDathangApiClient _dathangApiClient;
        private readonly IModelApiClient _modelApiClient;

        public DathangController(IDathangApiClient dathangApiClient, IModelApiClient modelApiClient, ILichsuthaotacApiClient lichsuthaotacApiClient)
        {
            _dathangApiClient = dathangApiClient;
            _modelApiClient = modelApiClient;
            _lichsuthaotacApiClient = lichsuthaotacApiClient;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.currentPage = "Quản lý đặt hàng";
            ViewBag.models = await _modelApiClient.GetAll();
            return View();
        }


        [HttpPost]
        private async Task<IActionResult> Luulichsu(string loaithaotac, string noidung, int? linhkienid, int? dathangid)
        {
            string hoten = HttpContext.Session.GetString("Token");
            var lichsuthaotac = new LichsuthaotacCreateRequest()
            {
                Nguoi = hoten,
                Loaithaotac = loaithaotac,
                Noidungthaotac = noidung,
                Linhkienid = linhkienid,
                Dathangid = dathangid
            };
            await _lichsuthaotacApiClient.Create(lichsuthaotac);
            return Ok();
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
                await Luulichsu("YCDH", "Tạo yêu cầu đặt hàng mới", null, result.ResultObj);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> XacNhanDatHang(int id, string ngaydukienhangve)
        {
            var request = new DathangXacNhanDatHangRequest()
            {
                ngaydukienhangve = ngaydukienhangve
            };
            var result = await _dathangApiClient.XacNhanDatHang(id, request);
            if (result.IsSuccessed)
            {
                await Luulichsu("XNDH", "Xác nhận bắt đầu đặt hàng", null, id);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = result.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> XacNhanHangVeDu(int id)
        {
            var result = await _dathangApiClient.XacNhanHangVeDu(id);
            if (result.IsSuccessed)
            {
                await Luulichsu("XNHVD", "Xác nhận hàng về đủ", null, id);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = result.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> XacNhanHangVeThieu(int id, XacNhanHangVeThieuRequest request)
        {
            var result = await _dathangApiClient.XacNhanHangVeThieu(id, request);
            if (result.IsSuccessed)
            {
                await Luulichsu("XNHVT", "Xác nhận hàng về thiếu", null, id);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = result.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> XacNhanHangVeDot2(int id)
        {
            var result = await _dathangApiClient.XacNhanHangVeDot2(id);
            if (result.IsSuccessed)
            {
                await Luulichsu("XNHVD2", "Xác nhận hàng về đợt 2", null, id);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = result.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string tenlinhkien, string tenmodel)
        {
            var result = await _dathangApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                await Luulichsu("XOADH", "Xoá dữ liệu liên quan đơn đặt hàng lình kiện: "+tenlinhkien+ " - " + tenmodel, null, null);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = result.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _dathangApiClient.GetById(id);
            return PartialView("_ChitietlichsuDonhang", data.ResultObj);
        }
    }
}
