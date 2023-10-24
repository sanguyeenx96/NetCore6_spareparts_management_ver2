using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.System.User;

namespace WebApp.Services
{
    public interface IDanhsachlinhkienApiClient
    {
        Task<ApiResult<List<DanhsachlinhkienVm>>> GetAll(GetDanhsachlinhkienRequest request);

        Task<ApiResult<PagedResult<DanhsachlinhkienVm>>> GetAllPaging(GetDanhsachlinhkienPagingRequest request);

        Task<ApiResult<DanhsachlinhkienVm>> GetById(int id);

        Task<ApiResult<bool>> Create(DanhsachlinhkienCreateRequest request);

        Task<ApiResult<ImportExcelResult>> ImportExcel(Stream file, string model);
    }
}
