using Data.EF;
using Microsoft.EntityFrameworkCore;
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
    public class DathangService : IDathangService
    {
        private readonly WebSparePartContext _context;
        public DathangService(WebSparePartContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<List<DathangVm>>> GetAll(GetDathangRequest request)
        {
            var query = _context.Dathangs.AsQueryable();
            if (!string.IsNullOrEmpty(request.Model))
            {
                query = query.Where(x => x.Model.Contains(request.Model));
            }
            if (!string.IsNullOrEmpty(request.Trangthai))
            {
                query = query.Where(x => x.Trangthai.Contains(request.Trangthai.Trim()));
            }
            var data = await query.Select(x => new DathangVm()
            {
                Ngayyeucau = x.Ngayyeucau,
                Ngaydathang = x.Ngaydathang,
                Ngaydukienhangve = x.Ngaydukienhangve,
                Ngayhangve = x.Ngayhangve,
                Ngaydukienhangvedot2 = x.Ngaydukienhangvedot2,
                Ngayhangvedot2 = x.Ngayhangvedot2,
                Tenjig = x.Tenjig,
                Majig = x.Majig,
                Tenlinhkien = x.Tenlinhkien,
                Malinhkien = x.Malinhkien,
                Maker = x.Maker,
                Soluongtonkho = x.Soluongtonkho,
                Soluong = x.Soluong,
                Donvi = x.Donvi,
                Dongia = x.Dongia,
                Thanhtien = x.Thanhtien,
                Ghichu = x.Ghichu,
                Trangthai = x.Trangthai,
                Nguoidathang = x.Nguoidathang,
                Image = x.Image,
                Model = x.Model,
                Soluongvedot1 = x.Soluongvedot1,
                Soluongvedot2 = x.Soluongvedot2,
            }).ToListAsync();
            return new ApiSuccessResult<List<DathangVm>>(data);
        }
    }
}


