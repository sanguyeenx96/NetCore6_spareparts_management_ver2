using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ViewModels.Danhsachlinhkien
{
    public class DanhsachlinhkienCreateRequest
    {
        [Display(Name = "Tên Model")]
        [Required(ErrorMessage ="Phải nhập tên Model")]
        public string Model { get; set; }

        [Display(Name = "Tên Jig")]
        public string Tenjig { get; set; }

        [Display(Name = "Mã Jig")]
        public string Majig { get; set; }

        [Display(Name = "Tên linh kiện")]
        public string Tenlinhkien { get; set; }

        [Display(Name = "Mã linh kiện")]
        public string Malinhkien { get; set; }

        [Display(Name = "Tên Maker")]
        public string Maker { get; set; }

        [Display(Name = "Đơn vị")]
        public string Donvi { get; set; }

        [Display(Name = "Đơn giá")]
        public int? Dongia { get; set; }

        [Display(Name = "Tồn kho")]
        public int? Tonkho { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Ghichu { get; set; }

        public IFormFile? Hinhanh { get;set; }

    }
}
