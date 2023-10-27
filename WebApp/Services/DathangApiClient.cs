﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
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

        public async Task<int> Count(GetDathangRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var response = await client.GetAsync($"/api/dathang/count?model={request.Model}" +
            $"&trangthai={request.Trangthai}");
            var result = await response.Content.ReadAsStringAsync();
            int count = Convert.ToInt32(result);
            return count;
        }

        public async Task<ApiResult<bool>> CreateYeuCauDatHang(int linhkienid, DathangCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/dathang/yeucaudathang/{linhkienid}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
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
