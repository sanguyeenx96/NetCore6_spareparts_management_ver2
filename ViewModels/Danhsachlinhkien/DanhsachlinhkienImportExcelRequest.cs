using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Danhsachlinhkien
{
    public class DanhsachlinhkienImportExcelRequest
    {
        public string? Tenjig { get; set; }
        public string? Majig { get; set; }
        public string? Tenlinhkien { get; set; }
        public string? Malinhkien { get; set; }
        public string? Maker { get; set; }
        public string? Donvi { get; set; }
        public int? Dongia { get; set; }
        public int? Tonkho { get; set; }
        public string? Ghichu { get; set; }
    }
}
