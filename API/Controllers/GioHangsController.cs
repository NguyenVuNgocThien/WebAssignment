using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangsController : ControllerBase
    {
        private qlbanhangContext context = new qlbanhangContext();
        public GioHangsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<GioHang> Get()
        {
            return context.GioHangs.Include(g=>g.MaKhNavigation).Include(g=>g.MaSpNavigation).ToList();
        }
        [HttpPost]
        public GioHang Post(GioHang gioHang)
        {
                context.Add(gioHang);
                context.SaveChanges();
            return gioHang;
        }
        [HttpPut]
        public GioHang Put(GioHang gioHang)
        {
            GioHang newgioHang = new GioHang();
            newgioHang = gioHang;
            newgioHang.ThanhTien = newgioHang.SoLuong * context.SanPhams.Find(gioHang.MaSp).Dongia;
            context.Update(gioHang);
            context.SaveChanges();
            return gioHang;
        }
        [HttpDelete]
        public IActionResult Delete(GioHang gioHang)
        {
            context.Remove(gioHang);
            context.SaveChanges();
            return NoContent();
        }
    }
}
