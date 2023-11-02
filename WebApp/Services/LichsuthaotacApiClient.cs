using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using ViewModels.Common;
using ViewModels.Lichsuthaotac.Request;
using ViewModels.Lichsuthaotac.Response;
using ViewModels.System.User;

namespace WebApp.Services
{
    public class LichsuthaotacApiClient : ILichsuthaotacApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public LichsuthaotacApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ApiResult<bool>> Create(LichsuthaotacCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/lichsuthaotac/", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<List<LichsuthaotacVm>>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var response = await client.GetAsync($"/api/lichsuthaotac/");

            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<LichsuthaotacVm>>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<LichsuthaotacVm>>>(body);
        }

        public async Task<ApiResult<List<LichsuthaotacVm>>> GetLichSu(GetLichsuthaotacRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/lichsuthaotac/data", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<LichsuthaotacVm>>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<LichsuthaotacVm>>>(result);
        }
    }
}
