using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;

namespace ViewModels.Danhsachlinhkien
{
    public class GetDanhsachlinhkienRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }

        public string? Model { get; set; }
    }
}
