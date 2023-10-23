using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Hinhanh
    {
        public int Id { get; set; }
        public int LinhkienId { get; set; }
        public string ImagePath { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public long FileSize { get; set; }

        public virtual Danhsachlinhkien Linhkien { get; set; } = null!;
    }
}
