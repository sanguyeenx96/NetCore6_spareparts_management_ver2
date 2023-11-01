using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Lichsuthaotac.Request;
using ViewModels.Lichsuthaotac.Response;

namespace Application.Lichsuthaotac
{
    public interface ILichsuthaotacService
    {
        Task<ApiResult<bool>> Create(LichsuthaotacCreateRequest request);
        Task<ApiResult<List<LichsuthaotacVm>>> GetAll();

    }
}
