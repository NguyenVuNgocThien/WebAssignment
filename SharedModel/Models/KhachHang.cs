using System;
using System.Collections.Generic;

#nullable disable

namespace SharedModel.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DanhGiaSanPhams = new HashSet<DanhGiaSanPham>();
            GioHangs = new HashSet<GioHang>();
            HoaDons = new HashSet<HoaDon>();
            TaiKhoanDangNhaps = new HashSet<TaiKhoanDangNhap>();
        }

        public string MaKh { get; set; }
        public string TenKh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }

        public virtual ICollection<DanhGiaSanPham> DanhGiaSanPhams { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<TaiKhoanDangNhap> TaiKhoanDangNhaps { get; set; }
    }
}
