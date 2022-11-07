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
    public class HoaDonsController : ControllerBase
    {
        private qlbanhangContext context = new qlbanhangContext();
        public HoaDonsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<HoaDon> Get()
        {
            return context.HoaDons.Include(h=>h.MaKhNavigation).Include(h=>h.MaNvNavigation).ToList();
        }
        [HttpGet("{id}")]
        public HoaDon Get(string id)
        {
            return context.HoaDons.Include(h => h.MaKhNavigation).Include(h => h.MaNvNavigation).FirstOrDefault(h=>h.MaHd==id);
        }
        [HttpPost]
        public HoaDon Post(HoaDon hoaDon)
        {
            hoaDon.MaHd = "HD"+ context.HoaDons.Count() + 1;
            hoaDon.NgayGiaoHang=DateTime.Now.AddDays(7);
            context.Add(hoaDon);
            context.SaveChanges();
            return hoaDon;
        }
    }
}
