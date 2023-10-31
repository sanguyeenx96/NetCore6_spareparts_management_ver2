using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Danhsachlinhkien;
using ViewModels.Danhsachlinhkien;
using ViewModels.Hinhanh;
using Microsoft.AspNetCore.Authorization;
using static Application.Danhsachlinhkien.DanhsachlinhkienService;
using Data.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhsachlinhkienController : ControllerBase
    {
        private readonly IDanhsachlinhkienService _danhsachlinhkienService;

        public DanhsachlinhkienController(IDanhsachlinhkienService danhsachlinhkienService)
        {
            _danhsachlinhkienService = danhsachlinhkienService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetDanhsachlinhkienRequest request)
        {
            var result = await _danhsachlinhkienService.GetAll(request);
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetDanhsachlinhkienPagingRequest request)
        {
            var result = await _danhsachlinhkienService.GetAllPaging(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _danhsachlinhkienService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DanhsachlinhkienCreateRequest request)
        {
            var result = await _danhsachlinhkienService.Create(request);
            return Ok(result);
        }

        ////Images
        //[HttpPost("{linhkienId}/images")]
        //public async Task<IActionResult> CreateImage(int linhkienId, [FromForm] HinhanhCreateRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var imageId = await _danhsachlinhkienService.AddImage(linhkienId, request);
        //    if (imageId == 0)
        //        return BadRequest();
        //    return Ok(imageId);
        //    //var image = await _danhsachlinhkienService.GetImageById(imageId);
        //    //return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        //}

        //[HttpDelete("{linhkienId}/images/{imageId}")]
        //public async Task<IActionResult> DeleteImage(int imageId)
        //{
        //    var result = await _danhsachlinhkienService.RemoveImage(imageId);
        //    if (result == 0)
        //        return BadRequest();
        //    return Ok(result);
        //}

        //[HttpPut("{linhkienId}/images/{imageId}")]
        //public async Task<IActionResult> UpdateImage(int imageId, [FromForm] HinhanhUpdateRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var result = await _danhsachlinhkienService.UpdateImage(imageId, request);
        //    if (result == 0)
        //        return BadRequest();
        //    return Ok();
        //}

        //[HttpGet("{linhkienId}/images/{imageId}")]
        //public async Task<IActionResult> GetImageById(int linhkienId, int imageId)
        //{
        //    var image = await _danhsachlinhkienService.GetImageById(imageId);
        //    if (image == null)
        //        return BadRequest("Cannot find product");
        //    return Ok(image);
        //}

        [HttpPost("importexcel")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImportExcel([FromForm] IFormFile file, [FromForm] string model)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Tệp Excel không được gửi.");
                }
                using (var fileStream = file.OpenReadStream())
                {
                    var danhsachlinhkiens = await _danhsachlinhkienService.ReadExcelFile(fileStream);
                    var result = await _danhsachlinhkienService.ImportExcelFile(danhsachlinhkiens, model);
                    if (result.IsSuccessed)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(result.Message);
                    }
                }      
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi xử lý tệp Excel: " + ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count([FromQuery] GetDanhsachlinhkienRequest request)
        {
            var result = await _danhsachlinhkienService.CountLinhkien(request);
            return Ok(result);
        }

        [HttpPut("laylinhkien/{id}")]
        public async Task<IActionResult> LayLinhKien(int id,[FromBody] LaylinhkienRequest request)
        {
            var result = await _danhsachlinhkienService.LayLinhKien(id,request);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromBody] DanhsachlinhkienUpdateRequest request)
        {
            var result = await _danhsachlinhkienService.Update(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _danhsachlinhkienService.Delete(id);
            return Ok(result);
        }

        [HttpPut("image/{id}")]
        public async Task<IActionResult> UpdateImage(int id, [FromBody] DanhsachlinhkienImageUpdateRequest request)
        {
            var result = await _danhsachlinhkienService.UpdateImage(id,request);
            return Ok(result);
        }
    }
}
