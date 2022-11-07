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

        public async Task<IActionResult> CreateAccount()
        {
            return View();
        }
        public async Task<IActionResult> Login(string username, string pass)
        {
            if (_context.TaiKhoanDangNhaps.Where(t=>t.MaQuyen==1).SingleOrDefault(t => t.TaiKhoan == username && t.MatKhau == pass) != null)
            {
                TaiKhoanDangNhap.currentUser = _context.TaiKhoanDangNhaps.SingleOrDefault(t => t.TaiKhoan == username && t.MatKhau == pass);
                LoginModel login = new LoginModel();
                login.TaiKhoan = username;
                login.MatKhau = pass;
                LoginModel.setLogin(login);
                var httpService = new HttpService(new HttpClient());
                var tokenResponse = await httpService.GetToken("https://localhost:44348/api/Login/Authenticate", LoginModel.loginModel);
                var ghs = await httpService.PostAsync<LoginModel>(url: "https://localhost:44348/api/Login/Authenticate", data: login);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Register(TaiKhoanDangNhap taiKhoanDangNhap,string retype)
        {
            if (_context.TaiKhoanDangNhaps.FirstOrDefault(t => t.TaiKhoan == taiKhoanDangNhap.TaiKhoan) == null && retype==taiKhoanDangNhap.MatKhau)
            {
                int i = _context.KhachHangs.Count()+1;
                var httpService = new HttpService(new HttpClient());
                taiKhoanDangNhap.MaKhNavigation.MaKh = i.ToString();
                taiKhoanDangNhap.MaKh = taiKhoanDangNhap.MaKhNavigation.MaKh;
                taiKhoanDangNhap.MaQuyen = 1;
                var ghs = await httpService.PostAsync<TaiKhoanDangNhap>(url: "https://localhost:44348/api/TaiKhoanDangNhaps", data: taiKhoanDangNhap);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NoContent();
            }
        }
        public async Task<IActionResult> Logout()
        {
            TaiKhoanDangNhap taiKhoanDangNhap = null;
            TaiKhoanDangNhap.setUser(taiKhoanDangNhap);
            return RedirectToAction("Index", "Home");
        }
    }
        
}
