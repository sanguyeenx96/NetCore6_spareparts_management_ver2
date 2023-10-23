using Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;
using ViewModels.Danhsachlinhkien;
using ViewModels.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Model
{
    public class ModelService : IModelService
    {
        private readonly WebSparePartContext _context;
        public ModelService(WebSparePartContext context)
        {
            _context = context;
        }

        public async Task<List<ModelVm>> GetAll()
        {
            var query = from c in _context.Models select c;
            return await query.Select(x => new ModelVm()
            {
                Id = x.Id,
                Tenmodel = x.Tenmodel
            }).ToListAsync();
        }
    }
}
