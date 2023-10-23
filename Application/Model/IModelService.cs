using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Model;

namespace Application.Model
{
    public interface IModelService
    {
        Task<List<ModelVm>> GetAll();

    }
}
