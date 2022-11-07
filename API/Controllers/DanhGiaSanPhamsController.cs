using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhGiaSanPhamsController : ControllerBase
    {
        private qlbanhangContext context = new qlbanhangContext();
        public DanhGiaSanPhamsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<DanhGiaSanPham> Get()
        {
            return context.DanhGiaSanPhams.Include(d => d.MaKhNavigation).Include(d => d.MaSpNavigation).ToList();
        }
        [HttpPost]
        public DanhGiaSanPham Post(DanhGiaSanPham danhGiaSanPham)
        {
            danhGiaSanPham.MaKh = TaiKhoanDangNhap.currentUser.MaKh;
            context.Add(danhGiaSanPham);
            context.SaveChanges();
            return danhGiaSanPham;
        }
    }
}
