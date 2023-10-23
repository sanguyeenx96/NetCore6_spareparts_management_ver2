using ViewModels.Common;
using ViewModels.Model;

namespace WebApp.Services
{
    public interface IModelApiClient
    {
        Task<List<ModelVm>> GetAll();

    }
}
