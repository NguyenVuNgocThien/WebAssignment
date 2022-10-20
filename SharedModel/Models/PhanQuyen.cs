using System;
using System.Collections.Generic;

#nullable disable

namespace SharedModel.Models
{
    public partial class PhanQuyen
    {
        public PhanQuyen()
        {
            TaiKhoanDangNhaps = new HashSet<TaiKhoanDangNhap>();
        }

        public int MaQuyen { get; set; }
        public string TenQuyen { get; set; }

        public virtual ICollection<TaiKhoanDangNhap> TaiKhoanDangNhaps { get; set; }
    }
}
