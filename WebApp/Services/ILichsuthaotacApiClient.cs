using ViewModels.Common;
using ViewModels.Lichsuthaotac.Request;
using ViewModels.Lichsuthaotac.Response;
using ViewModels.System.User;

namespace WebApp.Services
{
    public interface ILichsuthaotacApiClient
    {
        Task<ApiResult<List<LichsuthaotacVm>>> GetAll();
        Task<ApiResult<bool>> Create(LichsuthaotacCreateRequest request);
        Task<ApiResult<List<LichsuthaotacVm>>> GetLichSu(GetLichsuthaotacRequest request);
    }
}
