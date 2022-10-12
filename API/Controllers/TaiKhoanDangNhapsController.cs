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
    public class TaiKhoanDangNhapsController : ControllerBase
    {
        private qlbanhangContext context = new qlbanhangContext();
        public TaiKhoanDangNhapsController(IConfiguration configuration)
        {

        }
        [HttpGet]
        public List<TaiKhoanDangNhap> Get()
        {
            return context.TaiKhoanDangNhaps.ToList();
        }
    }
}
