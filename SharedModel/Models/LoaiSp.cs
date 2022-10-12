using System;
using System.Collections.Generic;

#nullable disable

namespace SharedModel.Models
{
    public partial class LoaiSp
    {
        public LoaiSp()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public int MaLoaiSp { get; set; }
        public string TenLoaiSp { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
