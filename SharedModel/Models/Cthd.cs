using System;
using System.Collections.Generic;

#nullable disable

namespace SharedModel.Models
{
    public partial class Cthd
    {
        public string MaHd { get; set; }
        public string MaSp { get; set; }
        public short? Soluong { get; set; }
        public float? DongiaBan { get; set; }
        public float? Giamgia { get; set; }

        public virtual HoaDon MaHdNavigation { get; set; }
        public virtual SanPham MaSpNavigation { get; set; }
    }
}
