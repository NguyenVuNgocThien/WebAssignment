using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedModel.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanphamsController : ControllerBase
    {
        private qlbanhangContext context=new qlbanhangContext();
        public SanphamsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<SanPham> Get()
        {
            return context.SanPhams.Select(s => s).ToList();
        }
        [HttpGet("{MaSP}")]
        public SanPham Get(string MaSP)
        {
            return context.SanPhams.FirstOrDefault(s => s.MaSp == MaSP);
        }
    }
}
