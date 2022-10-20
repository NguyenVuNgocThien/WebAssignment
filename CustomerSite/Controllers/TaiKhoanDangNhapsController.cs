using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            var qlbanhangContext = _context.TaiKhoanDangNhaps.Include(t => t.MaKhNavigation).Include(t => t.MaNvNavigation).Include(t => t.MaQuyenNavigation);
            return View(await qlbanhangContext.ToListAsync());
        }

        // GET: TaiKhoanDangNhaps/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoanDangNhap = await _context.TaiKhoanDangNhaps
                .Include(t => t.MaKhNavigation)
                .Include(t => t.MaNvNavigation)
                .Include(t => t.MaQuyenNavigation)
                .FirstOrDefaultAsync(m => m.TaiKhoan == id);
            if (taiKhoanDangNhap == null)
            {
                return NotFound();
            }

            return View(taiKhoanDangNhap);
        }

        // GET: TaiKhoanDangNhaps/Create
        public IActionResult Create()
        {
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh");
            ViewData["MaNv"] = new SelectList(_context.Nhanviens, "MaNv", "MaNv");
            ViewData["MaQuyen"] = new SelectList(_context.PhanQuyens, "MaQuyen", "MaQuyen");
            return View();
        }

        // POST: TaiKhoanDangNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaiKhoan,MatKhau")] TaiKhoanDangNhap taiKhoanDangNhap)
        {
            taiKhoanDangNhap.MaQuyen = 1;
            taiKhoanDangNhap.MaQuyenNavigation = _context.PhanQuyens.FirstOrDefault(p=>p.MaQuyen==1);
            var httpService = new HttpService(new HttpClient());
            var ghs = await httpService.PostAsync<TaiKhoanDangNhap>(url: "https://localhost:44327/api/TaiKhoanDangNhaps", data: taiKhoanDangNhap);
            return View(taiKhoanDangNhap);
        }

        // GET: TaiKhoanDangNhaps/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoanDangNhap = await _context.TaiKhoanDangNhaps.FindAsync(id);
            if (taiKhoanDangNhap == null)
            {
                return NotFound();
            }
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", taiKhoanDangNhap.MaKh);
            ViewData["MaNv"] = new SelectList(_context.Nhanviens, "MaNv", "MaNv", taiKhoanDangNhap.MaNv);
            ViewData["MaQuyen"] = new SelectList(_context.PhanQuyens, "MaQuyen", "MaQuyen", taiKhoanDangNhap.MaQuyen);
            return View(taiKhoanDangNhap);
        }

        // POST: TaiKhoanDangNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TaiKhoan,MatKhau,MaKh,MaNv,MaQuyen")] TaiKhoanDangNhap taiKhoanDangNhap)
        {
            if (id != taiKhoanDangNhap.TaiKhoan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoanDangNhap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanDangNhapExists(taiKhoanDangNhap.TaiKhoan))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", taiKhoanDangNhap.MaKh);
            ViewData["MaNv"] = new SelectList(_context.Nhanviens, "MaNv", "MaNv", taiKhoanDangNhap.MaNv);
            ViewData["MaQuyen"] = new SelectList(_context.PhanQuyens, "MaQuyen", "MaQuyen", taiKhoanDangNhap.MaQuyen);
            return View(taiKhoanDangNhap);
        }

        // GET: TaiKhoanDangNhaps/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoanDangNhap = await _context.TaiKhoanDangNhaps
                .Include(t => t.MaKhNavigation)
                .Include(t => t.MaNvNavigation)
                .Include(t => t.MaQuyenNavigation)
                .FirstOrDefaultAsync(m => m.TaiKhoan == id);
            if (taiKhoanDangNhap == null)
            {
                return NotFound();
            }

            return View(taiKhoanDangNhap);
        }

        // POST: TaiKhoanDangNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var taiKhoanDangNhap = await _context.TaiKhoanDangNhaps.FindAsync(id);
            _context.TaiKhoanDangNhaps.Remove(taiKhoanDangNhap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoanDangNhapExists(string id)
        {
            return _context.TaiKhoanDangNhaps.Any(e => e.TaiKhoan == id);
        }
    }
}
