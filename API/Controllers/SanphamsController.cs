using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanphamsController : ControllerBase
    {
        private qlbanhangContext context = new qlbanhangContext();
        public SanphamsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<SanPham> Get()
        {
            return context.SanPhams.Include(s => s.MaLoaiSpNavigation).ToList();
        }
        [HttpGet("{MaSP}")]
        public SanPham Get(string MaSP)
        {
            return context.SanPhams.Include(s => s.MaLoaiSpNavigation).FirstOrDefault(s => s.MaSp == MaSP);
        }
        [HttpGet("LoaiSP/{id}")]
        public List<SanPham> Get(int id)
        {
            if (id > 0 && id <= context.LoaiSps.Count())
            {
                return context.SanPhams.Include(s => s.MaLoaiSpNavigation).Where(s => s.MaLoaiSp == id).ToList();
            }
            else if (id > context.LoaiSps.Count())
            {
                return context.SanPhams.Include(s => s.MaLoaiSpNavigation).Where(s => s.NgayTao.Value.Year == DateTime.Today.Year && s.NgayTao.Value.Month == DateTime.Today.Month && (DateTime.Today.Day - s.NgayTao.Value.Day) < 7).ToList();
            }
            else
            {
                return context.SanPhams.Include(s => s.MaLoaiSpNavigation).ToList();
            }
        }
        [HttpGet("SP/{tenSP}")]
        public List<SanPham> GetByName(string tenSP)
        {
            return context.SanPhams.Include(s => s.MaLoaiSpNavigation).Where(s => s.TenSp.ToUpper().Contains(tenSP.ToUpper())).ToList();
        }
        [HttpPut]
        public SanPham Patch(SanPham sanPham)
        {
            context.Update(sanPham);
            context.SaveChanges();
            return sanPham;
        }
        [HttpDelete("del/{MaSp}")]
        public SanPham Delete(string MaSp)
        {
            SanPham sanPham = new SanPham();
            GioHang gioHang = new GioHang();
            Cthd cthd = new Cthd();
            sanPham = context.SanPhams.FirstOrDefault(s => s.MaSp == MaSp);
            gioHang = context.GioHangs.FirstOrDefault(g => g.MaSp == MaSp);
            cthd = context.Cthds.FirstOrDefault(c => c.MaSpNavigation.MaSp == MaSp);
            if (sanPham != null)
            {
                if ( gioHang== null)
                {
                    if (cthd == null)
                    {
                        context.Remove(sanPham);
                        context.SaveChanges();
                        return null;
                    }
                }
            }
            return sanPham;
        }
        [HttpPost]
        public SanPham Post(SanPham sanPham)
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 1000);
            sanPham.MaSp = i.ToString();
            context.Add(sanPham);
            context.SaveChanges();
            return sanPham;
        }
    }
}
