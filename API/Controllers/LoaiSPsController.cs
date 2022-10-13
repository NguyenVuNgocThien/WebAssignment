using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return context.LoaiSps.ToList();
        }
    }
}
