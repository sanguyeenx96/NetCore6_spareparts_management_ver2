using Data.EF;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Dathang;
using ViewModels.Lichsuthaotac.Request;
using ViewModels.Lichsuthaotac.Response;

namespace Application.Lichsuthaotac
{
    public class LichsuthaotacService : ILichsuthaotacService
    {
        private readonly WebSparePartContext _context;

        public LichsuthaotacService(WebSparePartContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> Create(LichsuthaotacCreateRequest request)
        {
            try
            {
                var lichsuthaotac = new Data.Models.Lichsuthaotac()
                {
                    Nguoi = request.Nguoi,
                    Loaithaotac = request.Loaithaotac,
                    Noidungthaotac = request.Noidungthaotac,
                    Thoigian = DateTime.Now,
                    Linhkienid = request.Linhkienid,
                    Dathangid = request.Dathangid
                };
                _context.Add(lichsuthaotac);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            catch
            {
                return new ApiErrorResult<bool>("Thêm lịch sử không thành công");

            }
        }

        public async Task<ApiResult<List<LichsuthaotacVm>>> GetAll()
        {
            var query = _context.Lichsuthaotacs.AsQueryable();
           
            var data = await query.Select(x => new LichsuthaotacVm()
            {
                Id = x.Id,
                Nguoi = x.Nguoi,
                Loaithaotac = x.Loaithaotac,
                Noidungthaotac = x.Noidungthaotac,
                Thoigian = x.Thoigian,
                Linhkienid = x.Linhkienid ,
                Dathangid = x.Dathangid 
            }).ToListAsync();
            return new ApiSuccessResult<List<LichsuthaotacVm>>(data);

        }

        public async Task<ApiResult<List<LichsuthaotacVm>>> GetLichsu(GetLichsuthaotacRequest request)
        {
            var query = _context.Lichsuthaotacs.AsQueryable();

            if(!request.Loaithaotacs.Any(item => item == "ALL"))
            {
                query = query.Where(x => request.Loaithaotacs.Contains(x.Loaithaotac));
            }
            var data = await query.Select(x => new LichsuthaotacVm()
            {
                Id = x.Id,
                Nguoi = x.Nguoi,
                Loaithaotac = x.Loaithaotac,
                Noidungthaotac = x.Noidungthaotac,
                Thoigian = x.Thoigian,
                Linhkienid = x.Linhkienid,
                Dathangid = x.Dathangid
            }).ToListAsync();
            return new ApiSuccessResult<List<LichsuthaotacVm>>(data);
        }
    }
}
