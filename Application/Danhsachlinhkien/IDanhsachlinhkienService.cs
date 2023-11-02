using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.Hinhanh;
using ViewModels.System.User;

namespace Application.Danhsachlinhkien
{
    public interface IDanhsachlinhkienService
    {
        Task<ApiResult<List<DanhsachlinhkienVm>>> GetAll(GetDanhsachlinhkienRequest request);
        Task<ApiResult<PagedResult<DanhsachlinhkienVm>>> GetAllPaging(GetDanhsachlinhkienPagingRequest request);
        Task<ApiResult<DanhsachlinhkienVm>> GetById(int id);
        Task<ApiResult<int>> Create(DanhsachlinhkienCreateRequest request);
        //Task<int> AddImage(int LinhkienId, HinhanhCreateRequest request);
        //Task<int> RemoveImage(int imageId);
        //Task<int> UpdateImage(int imageId, HinhanhUpdateRequest request);
        //Task<HinhanhVm> GetImageById(int imageId);
        Task<List<DanhsachlinhkienImportExcelRequest>> ReadExcelFile(Stream fileStream);
        Task<ApiResult<ImportExcelResult>> ImportExcelFile(List<DanhsachlinhkienImportExcelRequest> request,string model);

        Task<int> CountLinhkien(GetDanhsachlinhkienRequest request);

        Task<ApiResult<bool>> LayLinhKien(int id, LaylinhkienRequest request);

        Task<ApiResult<bool>> Update(DanhsachlinhkienUpdateRequest request);

        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<bool>> UpdateImage(int id, DanhsachlinhkienImageUpdateRequest request);


    }
}
