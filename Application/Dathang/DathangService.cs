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

        public async Task<int> CountDatHang(GetDathangRequest request)
        {
            var query = _context.Dathangs.AsQueryable();
            if (!string.IsNullOrEmpty(request.Model))
            {
                query = query.Where(x => x.Model.Contains(request.Model));
            }
            if (!string.IsNullOrEmpty(request.Trangthai))
            {
                query = query.Where(x => x.Trangthai == request.Trangthai);
            }
            int totalRows = await query.CountAsync();
            return totalRows;
        }

        public async Task<ApiResult<bool>> CreateYeuCauDatHang(int linhkienid, DathangCreateRequest request)
        {
            if (request.Soluong < 1)
            {
                return new ApiErrorResult<bool>("Số lượng không phù hợp");
            }
            try
            {
                var thongtinlinhkien = await _context.Danhsachlinhkiens.Where(x => x.Id == linhkienid).FirstOrDefaultAsync();
                if (thongtinlinhkien == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy thông tin linh kiện");
                }
                var yeucaudathang = new Data.Models.Dathang()
                {
                    Ngayyeucau = DateTime.Now.ToString("yyyy/MM/dd"),
                    Nguoidathang = request.Nguoithaotac,
                    Trangthai = "Yêu cầu đặt hàng",
                    Soluong = request.Soluong,
                    Thanhtien = (thongtinlinhkien.Donvi != null && thongtinlinhkien.Dongia != null)
                    ? request.Soluong * thongtinlinhkien.Dongia : null,
                    Ngaydathang = null,
                    Ngaydukienhangve = null,
                    Ngayhangve = null,
                    Ngaydukienhangvedot2 = null,
                    Ngayhangvedot2 = null,
                    Tenjig = thongtinlinhkien.Tenjig,
                    Majig = thongtinlinhkien.Majig,
                    Tenlinhkien = thongtinlinhkien.Tenlinhkien,
                    Malinhkien = thongtinlinhkien.Malinhkien,
                    Maker = thongtinlinhkien.Maker,
                    Soluongtonkho = thongtinlinhkien.Tonkho,
                    Donvi = thongtinlinhkien.Donvi,
                    Dongia = thongtinlinhkien.Dongia,                    
                    Ghichu = thongtinlinhkien.Ghichu,
                    Image = thongtinlinhkien.Image,
                    Model = thongtinlinhkien.Model,
                    Soluongvedot1 = null,
                    Soluongvedot2 = null,

                }; 
                await _context.Dathangs.AddAsync(yeucaudathang);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            catch
            {
                return new ApiErrorResult<bool>("Yêu cầu đặt hàng không thành công");
            }
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
                Id = x.Id,
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


