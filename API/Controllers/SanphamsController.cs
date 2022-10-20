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
            return context.SanPhams.Include(s => s.MaLoaiSpNavigation).Where(s => s.MaLoaiSp == id).ToList();
        }
    }
}
