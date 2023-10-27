using Microsoft.AspNetCore.Mvc;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.Dathang;

namespace WebApp.Services
{
    public interface IDathangApiClient 
    {
        Task<ApiResult<List<DathangVm>>> GetAll(GetDathangRequest request);
        Task<int> Count(GetDathangRequest request);
        Task<ApiResult<bool>> CreateYeuCauDatHang(int linhkienid,DathangCreateRequest request);

    }
}
