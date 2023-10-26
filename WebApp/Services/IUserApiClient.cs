using ViewModels.Common;
using ViewModels.System.User;

namespace WebApp.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<UserVm>> Authenticate(LoginRequest request);

        Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<bool>> RegisterUser(RegisterRequest register);

        Task<ApiResult<bool>> UpdateUser(int id,UserUpdateRequest request);
        Task<ApiResult<bool>> EditRoleUser(int id, UserEditRoleRequest request);

        Task<ApiResult<UserVm>> GetUserById(int id);

        Task<ApiResult<bool>> Delete(int id);


    }
}
