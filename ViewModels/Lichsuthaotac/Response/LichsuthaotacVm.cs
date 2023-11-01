using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Lichsuthaotac.Response
{
    public class LichsuthaotacVm
    {
        public int Id { get; set; }
        public string Nguoi { get; set; }
        public string Loaithaotac { get; set; }
        public string Noidungthaotac { get; set; } 
        public DateTime Thoigian { get; set; }
        public int? Linhkienid { get; set; }
        public int? Dathangid { get; set; }
    }
}
