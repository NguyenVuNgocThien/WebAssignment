using System;
using System.Collections.Generic;

#nullable disable

namespace SharedModel.Models
{
    public partial class GioHang
    {
        public string MaSp { get; set; }
        public string MaKh { get; set; }
        public int? SoLuong { get; set; }
        public double? ThanhTien { get; set; }
        public bool? IsDatHang { get; set; }

        public virtual KhachHang MaKhNavigation { get; set; }
        public virtual SanPham MaSpNavigation { get; set; }
    }
}
