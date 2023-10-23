using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;

namespace ViewModels.System.User
{
    public class GetUserPagingRequest :PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}
