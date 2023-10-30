using Application.Dathang;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Danhsachlinhkien;
using ViewModels.Dathang;
using ViewModels.Dathang.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DathangController : ControllerBase
    {
        private readonly IDathangService _dathangService;
        public DathangController(IDathangService dathangService)
        {
            _dathangService = dathangService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetDathangRequest request)
        {
            var result = await _dathangService.GetAll(request);
            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count([FromQuery] GetDathangRequest request)
        {
            var result = await _dathangService.CountDatHang(request);
            return Ok(result);
        }

        [HttpPost("yeucaudathang/{linhkienid}")]
        public async Task<IActionResult> CreateYeuCauDatHang(int linhkienid, [FromBody] DathangCreateRequest request)
        {
            var result = await _dathangService.CreateYeuCauDatHang(linhkienid, request);
            return Ok(result);
        }

        [HttpPut("xacnhandathang/{id}")]
        public async Task<IActionResult> XacNhanDatHang(int id, [FromBody] DathangXacNhanDatHangRequest request)
        {
            var result = await _dathangService.XacNhanDatHang(id, request);
            return Ok(result);
        }

        [HttpPost("xacnhanhangvedu/{id}")]
        public async Task<IActionResult> XacNhanHangVeDu(int id)
        {
            var result = await _dathangService.XacNhanHangVeDu(id);
            return Ok(result);
        }
        [HttpPut("xacnhanhangvethieu/{id}")]
        public async Task<IActionResult> XacNhanHangVeThieu(int id, [FromBody] XacNhanHangVeThieuRequest request)
        {
            var result = await _dathangService.XacNhanHangVeThieu(id,request);
            return Ok(result);
        }

        [HttpPut("xacnhanhangvedot2/{id}")]
        public async Task<IActionResult> XacNhanHangVeDot2(int id)
        {
            var result = await _dathangService.XacNhanHangVeDot2(id);
            return Ok(result);
        }

        [HttpDelete]
        public async Task

    }
}
