using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangsController : ControllerBase
    {
        private qlbanhangContext context = new qlbanhangContext();
        public KhachHangsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<KhachHang> Get()
        {
            return context.KhachHangs.ToList();
        }
        [HttpGet("{id}")]
        public KhachHang GetByID(string id)
        {
            return context.KhachHangs.FirstOrDefault(k=>k.MaKh == id);
        }
        [HttpGet("DanhSachKhachHangDangOrder")]
        public List<KhachHang> GetCustomerOrder()
        {
            List<KhachHang> khachHangs = context.KhachHangs.ToList();
            List<KhachHang> khs = new List<KhachHang>();
            foreach(KhachHang kh in khachHangs)
            {
                if (context.GioHangs.Where(g=>g.IsDatHang==true).FirstOrDefault(g => g.MaKh == kh.MaKh) != null)
                {
                    khs.Add(kh);
                }
            }
            return khs;
        }
        [HttpPost]
        public KhachHang Post(KhachHang kh)
        {
            context.Add(kh);
            context.SaveChanges();
            return kh;
        }
    }
}
