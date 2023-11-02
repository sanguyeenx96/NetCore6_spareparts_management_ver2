using Microsoft.AspNetCore.Mvc;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.Dathang;
using ViewModels.Dathang.Request;

namespace WebApp.Services
{
    public interface IDathangApiClient
    {
        Task<ApiResult<List<DathangVm>>> GetAll(GetDathangRequest request);

        Task<int> Count(GetDathangRequest request);

        Task<ApiResult<int>> CreateYeuCauDatHang(int linhkienid, DathangCreateRequest request);

        Task<ApiResult<bool>> XacNhanDatHang(int id, DathangXacNhanDatHangRequest request);

        Task<ApiResult<bool>> XacNhanHangVeDu(int id);

        Task<ApiResult<bool>> XacNhanHangVeThieu(int id, XacNhanHangVeThieuRequest request);

        Task<ApiResult<bool>> XacNhanHangVeDot2(int id);

        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<DathangVm>> GetById(int id);

    }
}
