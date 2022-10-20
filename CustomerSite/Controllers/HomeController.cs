using CustomerSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly qlbanhangContext context = new qlbanhangContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int maloaisp=0,string Tim="")
        {
            if (Tim != null)
            {
                var sps = context.SanPhams.Include(s => s.MaLoaiSpNavigation).Where(s => s.TenSp.ToUpper().Contains(Tim.ToUpper()));
                return View(sps.ToList());
            }
            else
            {
                if (maloaisp == 0)
                {
                    var sps = context.SanPhams.Include(s => s.MaLoaiSpNavigation);
                    return View(sps.ToList());
                }
                else
                {
                    var sps = context.SanPhams.Include(s => s.MaLoaiSpNavigation).Where(s => s.MaLoaiSp == maloaisp);
                    return View(sps.ToList());
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
