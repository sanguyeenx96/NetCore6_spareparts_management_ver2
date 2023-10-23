using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Danhsachlinhkien;

namespace ApiIntegration
{
    public class DanhsachlinhkienApiClient : BaseApiClient, IDanhsachlinhkienApiClient
    {
        public DanhsachlinhkienApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<List<CategoryVm>> GetAll(string languageId)
        {
            return await GetListAsync<CategoryVm>("/api/categories?languageId=" + languageId);
        }

        public async Task<CategoryVm> GetById(string languageId, int id)
        {
            return await GetAsync<CategoryVm>($"/api/categories/{id}/{languageId}");
        }

        public Task<DanhsachlinhkienVm> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<DanhsachlinhkienVm> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
