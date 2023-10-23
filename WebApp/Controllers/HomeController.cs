using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;
using ViewModels.Danhsachlinhkien;
using WebApp.Models;
using WebApp.Services;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDanhsachlinhkienApiClient _danhsachlinhkienApiClient;
        private readonly IModelApiClient _modelApiClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IDanhsachlinhkienApiClient danhsachlinhkienApiClient, IModelApiClient modelApiClient)
        {
            _logger = logger;
            _danhsachlinhkienApiClient = danhsachlinhkienApiClient;
            _modelApiClient = modelApiClient; 
        }

        public async Task<IActionResult> Index(string model, string? keyword)
        {
            var request = new GetDanhsachlinhkienRequest()
            {
                Keyword = !string.IsNullOrEmpty(keyword) ? keyword.Trim().ToLower() : "",
                Model = model
            };
            var result = await _danhsachlinhkienApiClient.GetAll(request);
            return View(result.ResultObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}