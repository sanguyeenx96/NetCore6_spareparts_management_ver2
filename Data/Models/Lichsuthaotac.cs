using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Lichsuthaotac
    {
        public int Id { get; set; }
        public string Nguoi { get; set; } = null!;
        public string Loaithaotac { get; set; } = null!;
        public string Noidungthaotac { get; set; } = null!;
        public DateTime Thoigian { get; set; }
        public int? Linhkienid { get; set; }
        public int? Dathangid { get; set; }

        public virtual Dathang? Dathang { get; set; }
        public virtual Danhsachlinhkien? Linhkien { get; set; }
    }
}
