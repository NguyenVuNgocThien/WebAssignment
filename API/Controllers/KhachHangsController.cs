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
        [HttpPost]
        public KhachHang Post(KhachHang kh)
        {
            context.Add(kh);
            context.SaveChanges();
            return kh;
        }
    }
}
