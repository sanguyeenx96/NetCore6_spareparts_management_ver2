using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Danhsachlinhkien;

namespace ApiIntegration
{
    public interface IDanhsachlinhkienApiClient
    {
        Task<DanhsachlinhkienVm> GetAll();
        Task<DanhsachlinhkienVm> GetById(int id);

    }
}
