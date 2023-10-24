using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.Dathang;

namespace Application.Dathang
{
    public interface IDathangService
    {
        Task<ApiResult<List<DathangVm>>> GetAll(GetDathangRequest request);
    }
}
