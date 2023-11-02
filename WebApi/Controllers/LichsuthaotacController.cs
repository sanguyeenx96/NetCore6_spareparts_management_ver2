using Application.Lichsuthaotac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Danhsachlinhkien;
using ViewModels.Lichsuthaotac.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichsuthaotacController : ControllerBase
    {
        private readonly ILichsuthaotacService _lichsuthaotacService;
        public LichsuthaotacController(ILichsuthaotacService lichsuthaotacService)
        {
            _lichsuthaotacService = lichsuthaotacService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LichsuthaotacCreateRequest request)
        {
            var result = await _lichsuthaotacService.Create(request);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _lichsuthaotacService.GetAll();
            return Ok(result);
        }
        [HttpPost("data")]
        public async Task<IActionResult> GetLichSu([FromBody] GetLichsuthaotacRequest request)
        {
            var result = await _lichsuthaotacService.GetLichsu(request);
            return Ok(result);
        }
    }
}
