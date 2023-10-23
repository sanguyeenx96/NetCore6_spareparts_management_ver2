using Newtonsoft.Json;
using ViewModels.Common;
using ViewModels.Model;
using ViewModels.System.User;

namespace WebApp.Services
{
    public class ModelApiClient : BaseApiClient, IModelApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ModelApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<List<ModelVm>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAdress"]);

            var response = await client.GetAsync($"/api/model");

            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<ModelVm>>(body);
            }
            return JsonConvert.DeserializeObject<List<ModelVm>>(body);
        }
    }
}
