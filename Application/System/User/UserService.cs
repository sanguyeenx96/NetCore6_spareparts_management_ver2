using Data.EF;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.System.User;
using WebSparePartContext = Data.EF.WebSparePartContext;

namespace Application.System.User
{
    public class UserService : IUserService
    {
        private readonly WebSparePartContext _context;
        public UserService(WebSparePartContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<UserVm>> Authencate(LoginRequest request)
        {
            var user = await _context.TblAdmins.Where(x => x.AdName == request.UserName).FirstOrDefaultAsync();
            if (user == null) return new ApiErrorResult<UserVm>("Tài khoản không tồn tại");

            var result = await _context.TblAdmins.Where(x => (x.AdName == request.UserName && x.AdPass == request.Password)).FirstOrDefaultAsync();
            if (result == null)
            {
                return new ApiErrorResult<UserVm>("Đăng nhập không đúng");
            }           
            var userLogin = new UserVm()
            {
                Id = result.AdId.ToString(),
                Hoten = result.Hoten,
                Username = result.AdName,
                Password = result.AdPass
            };
            return new ApiSuccessResult<UserVm>(userLogin);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            try
            {
                var user = await _context.TblAdmins.FindAsync(id);
                if (user == null) return new ApiErrorResult<bool>("Tài khoản không tồn tại");
                _context.TblAdmins.Remove(user);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            catch
            {
                return new ApiErrorResult<bool>("Xoá không thành công");
            }

        }

        public async Task<ApiResult<UserVm>> GetById(int id)
        {
            var result = await _context.TblAdmins.FindAsync(id);
            if (result == null)
            {
                return new ApiErrorResult<UserVm>("User không tồn tại");
            }
            var user = new UserVm()
            {
                Id = result.AdId.ToString(),
                Hoten = result.Hoten,
                Username = result.AdName,
                Password = result.AdPass
            };
            return new ApiSuccessResult<UserVm>(user);
        }

        public async Task<ApiResult<PagedResult<UserVm>>> GetUserPaging(GetUserPagingRequest request)
        {
            var query = _context.TblAdmins.AsQueryable(); // Chắc chắn query có kiểu IQueryable
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => (x.AdName.Contains(request.Keyword) || x.Hoten.Contains(request.Keyword)));
            }
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVm()
                {
                    Id = x.AdId.ToString(),
                    Hoten = x.Hoten,
                    Username = x.AdName,
                    Password = x.AdPass
                })
                .ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserVm>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            try
            {
                var user = await _context.TblAdmins.Where(x => x.AdName == request.UserName).FirstOrDefaultAsync();
                if (user != null)
                {
                    return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
                }
                user = new TblAdmin()
                {
                    AdName = request.Name,
                    AdPass = request.UserName,
                    Hoten = request.Password
                };
                await _context.TblAdmins.AddAsync(user);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            catch
            {
                return new ApiErrorResult<bool>("Đăng ký không thành công");
            }
        }

        public async Task<ApiResult<bool>> Update(int id, UserUpdateRequest request)
        {
            try
            {
                var user = await _context.TblAdmins.FindAsync(id);
                user.Hoten = request.Name;
                user.AdPass = request.Password;

                _context.TblAdmins.Update(user);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            catch
            {
                return new ApiErrorResult<bool>("Cập nhật không thành công");
            }
        }
    }
}
