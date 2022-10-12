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
    public class CTHDsController : ControllerBase
    {
        private qlbanhangContext context = new qlbanhangContext();
        public CTHDsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<Cthd> Get()
        {
            return context.Cthds.ToList();
        }
    }
}
