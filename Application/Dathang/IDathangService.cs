using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.Dathang;
using ViewModels.Dathang.Request;

namespace Application.Dathang
{
    public interface IDathangService
    {
        Task<ApiResult<List<DathangVm>>> GetAll(GetDathangRequest request);
        Task<int> CountDatHang(GetDathangRequest request);
        Task<ApiResult<bool>> CreateYeuCauDatHang(int linhkienid, DathangCreateRequest request);
        Task<ApiResult<bool>> XacNhanDatHang(int id, DathangXacNhanDatHangRequest request);
        Task<ApiResult<bool>> XacNhanHangVeDu(int id);
        Task<ApiResult<bool>> XacNhanHangVeThieu(int id, XacNhanHangVeThieuRequest request);
        Task<ApiResult<bool>> XacNhanHangVeDot2(int id);
        Task<ApiResult<bool>> XoaYeuCauDatHang(int id);
    }
}
