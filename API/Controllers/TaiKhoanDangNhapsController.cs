using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanDangNhapsController : ControllerBase
    {
        private qlbanhangContext context = new qlbanhangContext();
        public TaiKhoanDangNhapsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<TaiKhoanDangNhap> GetList()
        {
            return context.TaiKhoanDangNhaps.Include(t => t.MaKhNavigation).Include(t => t.MaNvNavigation).ToList();
        }
        [HttpGet("TaiKhoan")]
        public TaiKhoanDangNhap Get(TaiKhoanDangNhap taiKhoanDangNhap)
        {
            return context.TaiKhoanDangNhaps.Include(t => t.MaKhNavigation).Include(t => t.MaNvNavigation).Where(t => t.TaiKhoan == taiKhoanDangNhap.TaiKhoan).FirstOrDefault(t => t.MatKhau == taiKhoanDangNhap.MatKhau);
        }
        [HttpPost]
        public TaiKhoanDangNhap Post(TaiKhoanDangNhap taiKhoanDangNhap)
        {
            context.Add(taiKhoanDangNhap);
            context.SaveChanges();
            return taiKhoanDangNhap;
        }
        [HttpGet("{username}")]
        public TaiKhoanDangNhap Get(string username)
        {
            return context.TaiKhoanDangNhaps.Include(t => t.MaKhNavigation).Include(t => t.MaNvNavigation).FirstOrDefault(t => t.TaiKhoan == username);
        }
    }
}
