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
            return View(await _context.LoaiSps.ToListAsync());
        }

        // GET: LoaiSps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSp = await _context.LoaiSps
                .FirstOrDefaultAsync(m => m.MaLoaiSp == id);
            if (loaiSp == null)
            {
                return NotFound();
            }

            return View(loaiSp);
        }

        // GET: LoaiSps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiSps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoaiSp,TenLoaiSp")] LoaiSp loaiSp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiSp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSp);
        }

        // GET: LoaiSps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSp = await _context.LoaiSps.FindAsync(id);
            if (loaiSp == null)
            {
                return NotFound();
            }
            return View(loaiSp);
        }

        // POST: LoaiSps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoaiSp,TenLoaiSp")] LoaiSp loaiSp)
        {
            if (id != loaiSp.MaLoaiSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSpExists(loaiSp.MaLoaiSp))
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
            return View(loaiSp);
        }

        // GET: LoaiSps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSp = await _context.LoaiSps
                .FirstOrDefaultAsync(m => m.MaLoaiSp == id);
            if (loaiSp == null)
            {
                return NotFound();
            }

            return View(loaiSp);
        }

        // POST: LoaiSps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiSp = await _context.LoaiSps.FindAsync(id);
            _context.LoaiSps.Remove(loaiSp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSpExists(int id)
        {
            return _context.LoaiSps.Any(e => e.MaLoaiSp == id);
        }
    }
}
