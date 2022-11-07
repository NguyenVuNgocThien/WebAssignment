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
    public class LoaiSPsController : ControllerBase
    {
        private qlbanhangContext context = new qlbanhangContext();
        public LoaiSPsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<LoaiSp> Get()
        {
            return context.LoaiSps.Include(l=>l.SanPhams).ToList();
        }
        [HttpGet("{maLoaiSp}")]
        public LoaiSp Get(int maLoaiSp)
        {
            return context.LoaiSps.Include(l => l.SanPhams).FirstOrDefault(l=>l.MaLoaiSp==maLoaiSp);
        }
        [HttpPost]
        public LoaiSp Post(LoaiSp loaiSp)
        {
            int count = context.LoaiSps.Count();
            loaiSp.MaLoaiSp = count + 1;
            context.Add(loaiSp);
            context.SaveChanges();
            return loaiSp;
        }
        [HttpDelete("{maLoaiSp}")]
        public LoaiSp Del(int maLoaiSp)
        {
            if (context.SanPhams.FirstOrDefault(s => s.MaLoaiSp == maLoaiSp) == null)
            {
                context.Remove(context.LoaiSps.FirstOrDefault(l => l.MaLoaiSp == maLoaiSp));
                context.SaveChanges();
            }
            return context.LoaiSps.FirstOrDefault(l => l.MaLoaiSp == maLoaiSp);
        }
       
    }
}
