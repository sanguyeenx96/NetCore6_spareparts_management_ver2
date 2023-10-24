using Application.Common;
using Data.EF;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utilities.Constants;
using Utilities.Exceptions;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.Hinhanh;
using ViewModels.System.User;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Danhsachlinhkien
{
    public class DanhsachlinhkienService : IDanhsachlinhkienService
    {
        private readonly WebSparePartContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private readonly IConfiguration _configuration;

        public DanhsachlinhkienService(WebSparePartContext context, IStorageService storageService, IConfiguration configuration)
        {
            _context = context;
            _storageService = storageService;
            _configuration = configuration;
        }

        public async Task<ApiResult<bool>> Create(DanhsachlinhkienCreateRequest request)
        {
            try
            {
                var check = await _context.Danhsachlinhkiens.Where(x => x.Malinhkien == request.Malinhkien).FirstOrDefaultAsync();
                if (check != null)
                {
                    return new ApiErrorResult<bool>("Mã linh kiện đã tồn tại");
                }
                var linhkienmoi = new Data.Models.Danhsachlinhkien()
                {
                    Model = request.Model,
                    Tenjig = request.Tenjig,
                    Majig = request.Majig,
                    Tenlinhkien = request.Tenjig,
                    Malinhkien = request.Malinhkien,
                    Maker = request.Maker,
                    Donvi = request.Donvi,
                    Dongia = request.Dongia,
                    Tonkho = request.Tonkho,
                    Ghichu = request.Ghichu,
                };
                if (request.Hinhanh != null)
                {
                    linhkienmoi.Hinhanhs = new List<Hinhanh>()
                    {
                        new Hinhanh()
                        {
                            DateCreated = DateTime.Now,
                            FileSize = request.Hinhanh.Length,
                            ImagePath = await this.SaveFile(request.Hinhanh),
                        }
                    };
                }
                await _context.Danhsachlinhkiens.AddAsync(linhkienmoi);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            catch
            {
                return new ApiErrorResult<bool>("Thêm linh kiện mới không thành công");
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<ApiResult<PagedResult<DanhsachlinhkienVm>>> GetAllPaging(GetDanhsachlinhkienPagingRequest request)
        {
            var query = _context.Danhsachlinhkiens.AsQueryable();
            //2.Filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Tenlinhkien.Contains(request.Keyword.Trim())
                 || x.Malinhkien.Contains(request.Keyword.Trim()));
            }
            if (!string.IsNullOrEmpty(request.Model))
            {
                query = query.Where(x => x.Model.Contains(request.Model));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DanhsachlinhkienVm()
                {
                    Id = x.Id,
                    Model = x.Model,
                    Tenjig = x.Tenjig,
                    Majig = x.Majig,
                    Tenlinhkien = x.Tenlinhkien,
                    Malinhkien = x.Malinhkien,
                    Maker = x.Maker,
                    Donvi = x.Donvi,
                    Dongia = x.Dongia,
                    Tonkho = x.Tonkho,
                    Ghichu = x.Ghichu,
                    Image = x.Image
                })
                .ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<DanhsachlinhkienVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<DanhsachlinhkienVm>>(pagedResult);
        }

        public async Task<ApiResult<DanhsachlinhkienVm>> GetById(int id)
        {
            var result = await _context.Danhsachlinhkiens.FindAsync(id);

            if (result == null)
            {
                return new ApiErrorResult<DanhsachlinhkienVm>("Linh kiện không tồn tại");
            }
            var linhkien = new DanhsachlinhkienVm()
            {
                Id = result.Id,
                Model = result.Model,
                Tenjig = result.Tenjig,
                Majig = result.Majig,
                Tenlinhkien = result.Tenlinhkien,
                Malinhkien = result.Malinhkien,
                Maker = result.Maker,
                Donvi = result.Donvi,
                Dongia = result.Dongia,
                Tonkho = result.Tonkho,
                Ghichu = result.Ghichu,
                Image = result.Image
            };
            return new ApiSuccessResult<DanhsachlinhkienVm>(linhkien);
        }

        public async Task<int> AddImage(int LinhkienId, HinhanhCreateRequest request)
        {
            var hinhanh = new Hinhanh()
            {
                DateCreated = DateTime.Now,
                LinhkienId = LinhkienId,
            };
            if (request.ImageFile != null)
            {
                hinhanh.ImagePath = await this.SaveFile(request.ImageFile);
                hinhanh.FileSize = request.ImageFile.Length;
            }
            _context.Hinhanhs.Add(hinhanh);
            await _context.SaveChangesAsync();
            return hinhanh.Id;
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var image = await _context.Hinhanhs.FindAsync(imageId);
            if (image == null)
            {
                throw new SparepartsException($"Cannot find an image with id {imageId}");
            }
            _context.Hinhanhs.Remove(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, HinhanhUpdateRequest request)
        {
            var image = await _context.Hinhanhs.FindAsync(imageId);
            if (image == null)
            {
                throw new SparepartsException($"Cannot find an image with id {imageId}");
            }
            if (request.ImageFile != null)
            {
                image.ImagePath = await this.SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;
            }
            _context.Hinhanhs.Update(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<HinhanhVm> GetImageById(int imageId)
        {
            var image = await _context.Hinhanhs.FindAsync(imageId);
            if (image == null)
            {
                throw new SparepartsException($"Cannot find an image with id {imageId}");
            }
            var viewModel = new HinhanhVm()
            {
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                LinhkienId = image.LinhkienId
            };
            return viewModel;
        }

        public async Task<ApiResult<List<DanhsachlinhkienVm>>> GetAll(GetDanhsachlinhkienRequest request)
        {
            var query = _context.Danhsachlinhkiens.AsQueryable();
            if (!string.IsNullOrEmpty(request.Model))
            {
                query = query.Where(x => x.Model.Contains(request.Model));
            }
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Tenlinhkien.Contains(request.Keyword.Trim())
                 || x.Malinhkien.Contains(request.Keyword.Trim()));
            }
            var data = await query.Select(x => new DanhsachlinhkienVm()
            {
                Id = x.Id,
                Model = x.Model,
                Tenjig = x.Tenjig,
                Majig = x.Majig,
                Tenlinhkien = x.Tenlinhkien,
                Malinhkien = x.Malinhkien,
                Maker = x.Maker,
                Donvi = x.Donvi,
                Dongia = x.Dongia,
                Tonkho = x.Tonkho,
                Ghichu = x.Ghichu,
                Image = x.Image
            })
                .ToListAsync();
            return new ApiSuccessResult<List<DanhsachlinhkienVm>>(data);
        }

        public async Task<List<DanhsachlinhkienImportExcelRequest>> ReadExcelFile(Stream fileStream)
        {
            try
            {
                using (var package = new ExcelPackage(fileStream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var danhsachlinhkiens = new List<DanhsachlinhkienImportExcelRequest>();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        string? tenjig = null;
                        if (worksheet.Cells[row, 1].Value != null)
                        {
                            tenjig = worksheet.Cells[row, 1].Value.ToString();
                        }
                        string? majig = null;
                        if (worksheet.Cells[row, 2].Value != null)
                        {
                            majig = worksheet.Cells[row, 2].Value.ToString();
                        }
                        string? tenlinhkien = null;
                        if (worksheet.Cells[row, 3].Value != null)
                        {
                            tenlinhkien = worksheet.Cells[row, 3].Value.ToString();
                        }
                        string? malinhkien = null;
                        if (worksheet.Cells[row, 4].Value != null)
                        {
                            malinhkien = worksheet.Cells[row, 4].Value.ToString();
                        }
                        string? maker = null;
                        if (worksheet.Cells[row, 5].Value != null)
                        {
                            maker = worksheet.Cells[row, 5].Value.ToString();
                        }
                        string? donvi = null;
                        if (worksheet.Cells[row, 6].Value != null)
                        {
                            donvi = worksheet.Cells[row, 6].Value.ToString();
                        }
                        int? dongia = null;
                        if (worksheet.Cells[row, 7].Value != null)
                        {
                            dongia = Convert.ToInt32(worksheet.Cells[row, 7].Value.ToString());
                        }
                        int? tonkho = null;
                        if (worksheet.Cells[row, 8].Value != null)
                        {
                            tonkho = Convert.ToInt32(worksheet.Cells[row, 8].Value.ToString());
                        }
                        string? ghichu = null;
                        if (worksheet.Cells[row, 9].Value != null)
                        {
                            ghichu = worksheet.Cells[row, 9].Value.ToString();
                        }
                        var linhkien = new DanhsachlinhkienImportExcelRequest
                        {
                            Tenjig = tenjig,
                            Majig = majig,
                            Tenlinhkien = tenlinhkien,
                            Malinhkien = malinhkien,
                            Maker = maker,
                            Donvi = donvi,
                            Dongia = dongia,
                            Tonkho = tonkho,
                            Ghichu = ghichu
                        };
                        danhsachlinhkiens.Add(linhkien);
                    }
                    return danhsachlinhkiens;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, bạn có thể log lỗi hoặc đưa ra thông báo.
                throw new Exception("Error while processing Excel file: " + ex.Message);
            }
        }

        private bool ChecktrungMalinhkien(string model, string malinhkien)
        {
            var result = _context.Danhsachlinhkiens.Where(x => (x.Model == model && x.Malinhkien.Contains(malinhkien))).Count();
            if(result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<ApiResult<ImportExcelResult>> ImportExcelFile(List<DanhsachlinhkienImportExcelRequest> request, string model)
        {
            try
            {
                var startTime = DateTime.Now;
                int lineupdate = 0;
                int linetrung = 0;

                string connectionString = _configuration.GetConnectionString(SystemConstants.MainConnectionString);
                // Create a DataTable to hold your data
                DataTable dataTable = new DataTable("Danhsachlinhkiens");
                dataTable.Columns.Add("model", typeof(string));
                dataTable.Columns.Add("tenjig", typeof(string));
                dataTable.Columns.Add("majig", typeof(string));
                dataTable.Columns.Add("tenlinhkien", typeof(string));
                dataTable.Columns.Add("malinhkien", typeof(string));
                dataTable.Columns.Add("maker", typeof(string));
                dataTable.Columns.Add("donvi", typeof(string));
                dataTable.Columns.Add("dongia", typeof(int));
                dataTable.Columns.Add("tonkho", typeof(int));
                dataTable.Columns.Add("ghichu", typeof(string));

                foreach (var linhkien in request)
                {
                    bool isMalinhkienExists = ChecktrungMalinhkien(model, linhkien.Malinhkien);
                    if (!isMalinhkienExists)
                    {
                        lineupdate++;
                        dataTable.Rows.Add(model, linhkien.Tenjig, linhkien.Majig, linhkien.Tenlinhkien, linhkien.Malinhkien, linhkien.Maker,
                       linhkien.Donvi, linhkien.Dongia, linhkien.Tonkho, linhkien.Ghichu);
                    }
                    else
                    {
                        linetrung++;
                    }
                }
                using (var bulkCopy = new SqlBulkCopy(connectionString))
                {
                    bulkCopy.DestinationTableName = "danhsachlinhkien"; // Thay thế bằng tên bảng SQL thực tế
                    // Đối với các cột không khớp trực tiếp với tên cột trong SQL, bạn có thể ánh xạ chúng.
                    bulkCopy.ColumnMappings.Add("model", "model");
                    bulkCopy.ColumnMappings.Add("tenjig", "tenjig");
                    bulkCopy.ColumnMappings.Add("majig", "majig");
                    bulkCopy.ColumnMappings.Add("tenlinhkien", "tenlinhkien");
                    bulkCopy.ColumnMappings.Add("malinhkien", "malinhkien");
                    bulkCopy.ColumnMappings.Add("maker", "maker");
                    bulkCopy.ColumnMappings.Add("donvi", "donvi");
                    bulkCopy.ColumnMappings.Add("dongia", "dongia");
                    bulkCopy.ColumnMappings.Add("tonkho", "tonkho");
                    bulkCopy.ColumnMappings.Add("ghichu", "ghichu");
                    // Thiết lập kích thước lô nếu cần
                    bulkCopy.BatchSize = 1000; // Điều chỉnh kích thước lô theo nhu cầu
                    var endTime = DateTime.Now;
                    var executionTime = endTime - startTime;
                    Console.WriteLine($"Thời gian thực thi: {executionTime}");
                    // Thực hiện sao chép dữ liệu vào SQL
                    await bulkCopy.WriteToServerAsync(dataTable);
                }
                var ketqua = new ImportExcelResult()
                {
                    sodongtrung = linetrung,
                    sodongupdate = lineupdate
                };
                return new ApiSuccessResult<ImportExcelResult>(ketqua);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while processing insert to SQL: " + ex.Message);
            }

        }
    }
}
