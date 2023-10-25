using Application.Dathang;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Danhsachlinhkien;
using ViewModels.Dathang;

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
    }
}
