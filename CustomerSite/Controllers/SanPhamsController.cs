using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedModel.Models;

namespace CustomerSite.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly ILogger<SanPhamsController> _logger;
        private readonly qlbanhangContext _context=new qlbanhangContext();

        public SanPhamsController(ILogger<SanPhamsController> logger)
        {
            _logger = logger;
        }

        // GET: SanPhams
        public async Task<IActionResult> Index()
        {
            var httpService = new HttpService(new System.Net.Http.HttpClient());
            var sps = await httpService.GetAsync<List<SanPham>>(url: "https://localhost:44348/api/Sanphams");
            return View(sps);
        }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qlbanhangContext = _context.SanPhams.Include(s => s.MaLoaiSpNavigation).FirstOrDefault(s=>s.MaSp==id);
            return View( qlbanhangContext);
        }

        
        // GET: SanPhams/Create
        public IActionResult Create()
        {
            ViewData["MaLoaiSp"] = new SelectList(_context.LoaiSps, "MaLoaiSp", "MaLoaiSp");
            return View();
        }
    }
}
