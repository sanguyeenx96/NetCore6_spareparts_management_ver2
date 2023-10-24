using Newtonsoft.Json;
using System.Net.Http;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.Dathang;

namespace WebApp.Services
{
    public class DathangApiClient : IDathangApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public DathangApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ApiResult<List<DathangVm>>> GetAll(GetDathangRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var response = await client.GetAsync($"/api/dathang?model={request.Model}" +
            $"&trangthai={request.Trangthai}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<DathangVm>>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<DathangVm>>>(body);
        }
    }
}
