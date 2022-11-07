using System;
using System.Collections.Generic;

#nullable disable

namespace SharedModel.Models
{
    public partial class DanhGiaSanPham
    {
        public string MaKh { get; set; }
        public string MaSp { get; set; }
        public int? DanhGia { get; set; }

        public virtual KhachHang MaKhNavigation { get; set; }
        public virtual SanPham MaSpNavigation { get; set; }
    }
}
