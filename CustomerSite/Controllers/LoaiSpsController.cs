using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SharedModel.Models;

namespace CustomerSite.Controllers
{
    public class LoaiSpsController : Controller
    {
        private readonly qlbanhangContext _context;

        public LoaiSpsController(qlbanhangContext context)
        {
            _context = context;
        }

        // GET: LoaiSps
        public async Task<IActionResult> Index()
        {
            var httpService = new HttpService(new System.Net.Http.HttpClient());
            var lsps = await httpService.GetAsync<List<SanPham>>(url: "https://localhost:44348/api/LoaiSPs");
            return View(lsps);
        }

        private bool LoaiSpExists(int id)
        {
            return _context.LoaiSps.Any(e => e.MaLoaiSp == id);
        }
    }
}
