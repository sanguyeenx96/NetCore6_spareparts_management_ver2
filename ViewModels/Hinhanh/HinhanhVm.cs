using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Hinhanh
{
    public class HinhanhVm
    {
        public int Id { get; set; }
        public int LinhkienId { get; set; }
        public string ImagePath { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public long FileSize { get; set; }
    }
}
