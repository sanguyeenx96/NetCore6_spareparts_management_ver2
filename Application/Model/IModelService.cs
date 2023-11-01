using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Model;
using ViewModels.Model.Request;

namespace Application.Model
{
    public interface IModelService
    {
        Task<List<ModelVm>> GetAll();
        Task<ApiResult<bool>> Create(ModelCreateRequest request);
        Task<ApiResult<bool>> Delete(int id);

    }
}
