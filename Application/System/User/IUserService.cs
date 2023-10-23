using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.System.User;

namespace Application.System.User
{
    public interface IUserService
    {
        Task<ApiResult<UserVm>> Authencate(LoginRequest request);

        Task<ApiResult<PagedResult<UserVm>>> GetUserPaging(GetUserPagingRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<bool>> Update(int id,UserUpdateRequest request);

        Task<ApiResult<UserVm>> GetById(int id);

        Task<ApiResult<bool>> Delete(int id);
    }
}
