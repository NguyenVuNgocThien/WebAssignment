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

        public async Task<IActionResult> Index(int maloaisp=0,string Tim="")
        {
            if (Tim != "")
            {
                var httpService = new HttpService(new System.Net.Http.HttpClient());
                var sps = await httpService.GetAsync<List<SanPham>>(url: "https://localhost:44348/api/Sanphams/SP/" + Tim);
                return View(sps);
            }
            else 
            {
                    var httpService = new HttpService(new System.Net.Http.HttpClient());
                    var sps = await httpService.GetAsync<List<SanPham>>(url: "https://localhost:44348/api/Sanphams/loaiSP/" + maloaisp);
                    return View(sps);
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
