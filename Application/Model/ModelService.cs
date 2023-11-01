using Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.Model;
using ViewModels.Model.Request;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Model
{
    public class ModelService : IModelService
    {
        private readonly WebSparePartContext _context;
        public ModelService(WebSparePartContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> Create(ModelCreateRequest request)
        {
            var newmodel = await _context.Models.Where(x => x.Tenmodel.Contains(request.Tenmodel)).FirstOrDefaultAsync();
            if (newmodel != null)
            {   
                return new ApiErrorResult<bool>("Đã tồn tại model này!");
            }
            try
            {
                newmodel = new Data.Models.Model()
                {
                    Tenmodel = request.Tenmodel.ToUpper(),
                };
                _context.Models.Add(newmodel);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            catch
            {
                return new ApiErrorResult<bool>("Thêm mới không thành công");
            }
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy model này!");
            }
            try
            {                
                _context.Models.Remove(model);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            catch
            {
                return new ApiErrorResult<bool>("Xoá Model không thành công");
            }
        }

        public async Task<List<ModelVm>> GetAll()
        {
            var query = from c in _context.Models select c;
            return await query.Select(x => new ModelVm()
            {
                Id = x.Id,
                Tenmodel = x.Tenmodel
            }).ToListAsync();
        }
    }
}
