using ViewModels.Common;
using ViewModels.Model;
using ViewModels.Model.Request;

namespace WebApp.Services
{
    public interface IModelApiClient
    {
        Task<List<ModelVm>> GetAll();
        Task<ApiResult<bool>> Create(ModelCreateRequest request);

        Task<ApiResult<bool>> Delete(int id);

    }
}
