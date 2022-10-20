using System;
using System.Collections.Generic;

#nullable disable

namespace SharedModel.Models
{
    public partial class Nhanvien
    {
        public Nhanvien()
        {
            HoaDons = new HashSet<HoaDon>();
            TaiKhoanDangNhaps = new HashSet<TaiKhoanDangNhap>();
        }

        public int MaNv { get; set; }
        public string HoNv { get; set; }
        public string Ten { get; set; }
        public string Diachi { get; set; }
        public string Dienthoai { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<TaiKhoanDangNhap> TaiKhoanDangNhaps { get; set; }
    }
}
