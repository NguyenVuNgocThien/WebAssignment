using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
            return context.GioHangs.Include(g=>g.MaSpNavigation).ToList();
        }
        [HttpGet("{MaKh}")]
        [Authorize]
        public List<GioHang> Get(string MaKh)
        {
            return context.GioHangs.Include(g => g.MaSpNavigation).Where(g => g.MaKh == MaKh).Where(g => g.IsDatHang == true).ToList();
        }
        [HttpDelete("del/{MaKh}")]
        [Authorize]
        public List<GioHang> Del(string MaKh)
        {
            List<GioHang> gioHangs = context.GioHangs.Include(g => g.MaSpNavigation).Where(g => g.MaKh == MaKh).Where(g => g.IsDatHang == true).ToList();
            foreach(GioHang gioHang in gioHangs)
            {
                context.Remove(gioHang);
                context.SaveChanges();
            }
            return gioHangs;
        }
        [HttpPost]
        [Authorize]
        public GioHang Post(GioHang gioHang)
        {
                context.Add(gioHang);
                context.SaveChanges();
            return gioHang;
        }
        [HttpPut]
        [Authorize]
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
        [Authorize]
        public IActionResult Delete(GioHang gioHang)
        {
            context.Remove(gioHang);
            context.SaveChanges();
            return NoContent();
        }
    }
}
