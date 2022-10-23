using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using SharedModel.Models;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.Http.Json;
using CustomerSite.Extensions;
using System.Net.Http.Headers;

namespace CustomerSite.Controllers
{
    public class GioHangsController : Controller
    {
        private readonly qlbanhangContext _context;

        public GioHangsController(qlbanhangContext context)
        {
            _context = context;
        }

        // GET: GioHangs
        public async Task<IActionResult> Index()
        {
            if (TaiKhoanDangNhap.currentUser != null)
            {
                var qlbanhangContext = _context.GioHangs.Include(g => g.MaKhNavigation).Include(g => g.MaSpNavigation).Where(g => g.MaKh == TaiKhoanDangNhap.currentUser.MaKh);
                return View(await qlbanhangContext.ToListAsync());
            }
            else
                return NoContent();
        }
        public async Task<RedirectToActionResult> AddToCart(string masp)
        {
            if (_context.GioHangs.Where(g=>g.MaKh==TaiKhoanDangNhap.currentUser.MaKh).FirstOrDefault(m => m.MaSp == masp) == null)
            {
                GioHang gioHang = new GioHang();
                gioHang.MaSp = masp;
                gioHang.MaKh = TaiKhoanDangNhap.currentUser.MaKh;
                gioHang.IsDatHang = false;
                gioHang.SoLuong = 1;
                gioHang.ThanhTien = _context.SanPhams.Find(masp).Dongia * gioHang.SoLuong;
                var httpService = new HttpService(new HttpClient());
                var ghs = await httpService.PostAsync<GioHang>(url: "https://localhost:44327/api/GioHangs", data: gioHang);
            }
            else
            {
                GioHang gioHang = new GioHang();
                gioHang = _context.GioHangs.Where(g => g.MaKh == TaiKhoanDangNhap.currentUser.MaKh).FirstOrDefault(m => m.MaSp == masp);
                if (gioHang != null)
                {
                    gioHang.SoLuong++;
                    var httpService = new HttpService(new HttpClient());
                    var ghs = await httpService.PutAsync<GioHang>(url: "https://localhost:44327/api/GioHangs", data: gioHang);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<RedirectToActionResult> Update(string MaSP, int txtSL)
        {
            GioHang gioHang = new GioHang();
            gioHang = _context.GioHangs.Where(m => m.MaSp == MaSP).FirstOrDefault(m => m.MaKh == TaiKhoanDangNhap.currentUser.MaKh);
            if (gioHang != null)
            {
                gioHang.SoLuong = txtSL;
                var httpService = new HttpService(new HttpClient());
                var ghs = await httpService.PutAsync<GioHang>(url: "https://localhost:44327/api/GioHangs", data: gioHang);
            }
            return RedirectToAction("Index");
        }
        public async Task<RedirectToActionResult> DelCartItem(string MaSP)
        {
            GioHang gioHang = new GioHang();
            gioHang = _context.GioHangs.Where(g => g.MaSp == MaSP).FirstOrDefault(g => g.MaKh == TaiKhoanDangNhap.currentUser.MaKh);
            if (gioHang != null)
            {
                var httpService = new HttpService(new HttpClient());
                var ghs = await httpService.DeleteAsync<GioHang>(url: "https://localhost:44327/api/GioHangs", data: gioHang);
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Order()
        {
            List<GioHang> ghs = _context.GioHangs.Where(g => g.MaKh == TaiKhoanDangNhap.currentUser.MaKh).Where(g=>g.IsDatHang==false).ToList();
            foreach (GioHang gh in ghs)
            {
                if (gh != null)
                {
                    gh.IsDatHang = true;
                    var httpService = new HttpService(new HttpClient());
                    var put = await httpService.PutAsync<GioHang>(url: "https://localhost:44327/api/GioHangs", data: gh);
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
