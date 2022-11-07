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
    public class CTHDsController : ControllerBase
    {
        private qlbanhangContext context = new qlbanhangContext();
        public CTHDsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<Cthd> Get()
        {
            return context.Cthds.Include(c=>c.MaHdNavigation).Include(c=>c.MaSpNavigation).ToList();
        }
        [HttpGet("{MaKh}")]
        public List<Cthd> Get(string MaKh)
        {
            return context.Cthds.Include(c => c.MaHdNavigation).Include(c => c.MaSpNavigation).Where(c => c.MaHdNavigation.MaKh == MaKh).ToList();
        }
        [HttpPost]
        public Cthd Post(Cthd cthd)
        {
            context.Add(cthd);
            context.SaveChanges();
            return cthd;
        }
    }
}
