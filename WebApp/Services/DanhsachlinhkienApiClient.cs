using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.System.User;

namespace WebApp.Services
{
    public class DanhsachlinhkienApiClient : BaseApiClient, IDanhsachlinhkienApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public DanhsachlinhkienApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<int> Count(GetDanhsachlinhkienRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);
            var response = await client.GetAsync($"/api/danhsachlinhkien/count?keyword={request.Keyword}" +
            $"&Model={request.Model}");
            var result = await response.Content.ReadAsStringAsync();
            int count = Convert.ToInt32(result);
            return count;
        }

        //public async Task<ApiResult<bool>> Create(DanhsachlinhkienCreateRequest request)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri(_configuration["BaseAdress"]);

        //    var requestContent = new MultipartFormDataContent();
        //    if (request.Hinhanh != null)
        //    {
        //        byte[] data;
        //        using (var br = new BinaryReader(request.Hinhanh.OpenReadStream()))
        //        {
        //            data = br.ReadBytes((int)request.Hinhanh.OpenReadStream().Length);
        //        }
        //        ByteArrayContent bytes = new ByteArrayContent(data);
        //        requestContent.Add(bytes, "Hinhanh", request.Hinhanh.FileName);
        //    }
        //    requestContent.Add(new StringContent(request.Model.ToString()), "model");
        //    requestContent.Add(new StringContent(request.Tenjig.ToString()), "tenjig");
        //    requestContent.Add(new StringContent(request.Majig.ToString()), "majig");
        //    requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Tenlinhkien) ? "" : request.Tenlinhkien.ToString()), "tenlinhkien");
        //    requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Malinhkien) ? "" : request.Malinhkien.ToString()), "malinhkien");
        //    requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Maker) ? "" : request.Maker.ToString()), "maker");
        //    requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Donvi) ? "" : request.Donvi.ToString()), "donvi");
        //    requestContent.Add(new StringContent(request.Dongia.ToString()), "dongia");
        //    requestContent.Add(new StringContent(request.Tonkho.ToString()), "tonkho");
        //    requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Ghichu) ? "" : request.Ghichu.ToString()), "ghichu");

        //    var response = await client.PostAsync($"/api/danhsachlinhkien", requestContent);
        //    var result = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
        //    }
        //    return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        //}

        public async Task<ApiResult<int>> Create(DanhsachlinhkienCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/danhsachlinhkien/", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<int>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<int>>(result);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var response = await client.DeleteAsync($"/api/danhsachlinhkien/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<List<DanhsachlinhkienVm>>> GetAll(GetDanhsachlinhkienRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var response = await client.GetAsync($"/api/danhsachlinhkien?keyword={request.Keyword}" +
            $"&Model={request.Model}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<DanhsachlinhkienVm>>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<DanhsachlinhkienVm>>>(body);
        }

        public async Task<ApiResult<PagedResult<DanhsachlinhkienVm>>> GetAllPaging(GetDanhsachlinhkienPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var response = await client.GetAsync($"/api/danhsachlinhkien/paging?pageIndex=" +
             $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}" +
             $"&Model={request.Model}");

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<DanhsachlinhkienVm>>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<PagedResult<DanhsachlinhkienVm>>>(body);
        }

        public async Task<ApiResult<DanhsachlinhkienVm>> GetById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var response = await client.GetAsync($"/api/danhsachlinhkien/{id}");

            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<DanhsachlinhkienVm>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<DanhsachlinhkienVm>>(body);
        }

        public async Task<ApiResult<ImportExcelResult>> ImportExcel(Stream fileStream, string model)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);
            var requestContent = new MultipartFormDataContent();
            // Tạo ByteArrayContent từ Stream
            var fileBytes = new byte[fileStream.Length];
            await fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = "excelfile.xlsx" // Tên tệp tin (có thể thay đổi)
            };
            requestContent.Add(fileContent);
            // Thêm tham số model vào yêu cầu
            requestContent.Add(new StringContent(model), "model");
            var response = await client.PostAsync($"/api/danhsachlinhkien/importexcel", requestContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<ImportExcelResult>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<ImportExcelResult>>(result);
        }

        public async Task<ApiResult<bool>> Laylinhkien(int id, LaylinhkienRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/danhsachlinhkien/laylinhkien/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> Update(DanhsachlinhkienUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/danhsachlinhkien/update/{request.Id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> UpdateImage(int id, DanhsachlinhkienImageUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/danhsachlinhkien/image/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
