using Microsoft.AspNetCore.Mvc;
using ViewModels.Common;
using ViewModels.Dathang;

namespace WebApp.Services
{
    public interface IDathangApiClient 
    {
        Task<ApiResult<List<DathangVm>>> GetAll(GetDathangRequest request);

    }
}
