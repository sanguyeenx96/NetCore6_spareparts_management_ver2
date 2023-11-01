using Application.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Model.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var danhsachmodel = await _modelService.GetAll();
            return Ok(danhsachmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ModelCreateRequest request)
        {
            var result = await _modelService.Create(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _modelService.Delete(id);
            return Ok(result);
        }
    }
}
