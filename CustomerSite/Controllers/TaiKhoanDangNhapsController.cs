using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SharedModel.Models;

namespace CustomerSite.Controllers
{
    public class TaiKhoanDangNhapsController : Controller
    {
        private readonly qlbanhangContext _context;

        public TaiKhoanDangNhapsController(qlbanhangContext context)
        {
            _context = context;
        }

        // GET: TaiKhoanDangNhaps
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string username, string pass)
        {
            if (_context.TaiKhoanDangNhaps.SingleOrDefault(t => t.TaiKhoan == username && t.MatKhau == pass) != null)
            {
                TaiKhoanDangNhap.currentUser = _context.TaiKhoanDangNhaps.SingleOrDefault(t => t.TaiKhoan == username && t.MatKhau == pass);
                LoginModel login = new LoginModel();
                login.TaiKhoan = username;
                login.MatKhau = pass;
                var httpService = new HttpService(new HttpClient());
                var ghs = await httpService.PostAsync<LoginModel>(url: "https://localhost:44327/api/Login/Authenticate", data: login);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NoContent();
            }
        }
    }
        
}
