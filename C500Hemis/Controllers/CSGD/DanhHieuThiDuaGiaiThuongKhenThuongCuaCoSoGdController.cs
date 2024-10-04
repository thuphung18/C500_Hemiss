using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C500Hemis.Models;

namespace C500Hemis.Controllers.CSGD
{
    public class DanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGdController : Controller
    {
        private readonly HemisContext _context;

        public DanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGdController(HemisContext context)
        {
            _context = context;
        }

        // GET: DanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd
        public async Task<IActionResult> Index()
        {
            var hemisContext = _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGds.Include(t => t.IdCapKhenThuongNavigation).Include(t => t.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGdNavigation).Include(t => t.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuongNavigation).Include(t => t.IdPhuongThucKhenThuongNavigation);
            return View(await hemisContext.ToListAsync());
        }

        // GET: DanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd = await _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGds
                .Include(t => t.IdCapKhenThuongNavigation)
                .Include(t => t.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGdNavigation)
                .Include(t => t.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuongNavigation)
                .Include(t => t.IdPhuongThucKhenThuongNavigation)
                .FirstOrDefaultAsync(m => m.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd == id);
            if (tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd == null)
            {
                return NotFound();
            }

            return View(tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
        }

        // GET: DanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd/Create
        public IActionResult Create()
        {
            ViewData["IdCapKhenThuong"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong");
            ViewData["IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd"] = new SelectList(_context.DmThiDuaGiaiThuongKhenThuongs, "IdThiDuaGiaiThuongKhenThuong", "IdThiDuaGiaiThuongKhenThuong");
            ViewData["IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmLoaiDanhHieuThiDuaGiaiThuongKhenThuongs, "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong");
            ViewData["IdPhuongThucKhenThuong"] = new SelectList(_context.DmPhuongThucKhenThuongs, "IdPhuongThucKhenThuong", "IdPhuongThucKhenThuong");
            return View();
        }

        // POST: DanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd,IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong,IdDanhHieuThiDuaGiaiThuongKhenThuong,SoQuyetDinhKhenThuong,IdPhuongThucKhenThuong,NamKhenThuong,IdCapKhenThuong")] TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCapKhenThuong"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdCapKhenThuong);
            ViewData["IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd"] = new SelectList(_context.DmThiDuaGiaiThuongKhenThuongs, "IdThiDuaGiaiThuongKhenThuong", "IdThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
            ViewData["IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmLoaiDanhHieuThiDuaGiaiThuongKhenThuongs, "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong);
            ViewData["IdPhuongThucKhenThuong"] = new SelectList(_context.DmPhuongThucKhenThuongs, "IdPhuongThucKhenThuong", "IdPhuongThucKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdPhuongThucKhenThuong);
            return View(tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
        }

        // GET: DanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd = await _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGds.FindAsync(id);
            if (tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd == null)
            {
                return NotFound();
            }
            ViewData["IdCapKhenThuong"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdCapKhenThuong);
            ViewData["IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd"] = new SelectList(_context.DmThiDuaGiaiThuongKhenThuongs, "IdThiDuaGiaiThuongKhenThuong", "IdThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
            ViewData["IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmLoaiDanhHieuThiDuaGiaiThuongKhenThuongs, "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong);
            ViewData["IdPhuongThucKhenThuong"] = new SelectList(_context.DmPhuongThucKhenThuongs, "IdPhuongThucKhenThuong", "IdPhuongThucKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdPhuongThucKhenThuong);
            return View(tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
        }

        // POST: DanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd,IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong,IdDanhHieuThiDuaGiaiThuongKhenThuong,SoQuyetDinhKhenThuong,IdPhuongThucKhenThuong,NamKhenThuong,IdCapKhenThuong")] TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd)
        {
            if (id != tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGdExists(tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd))
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
            ViewData["IdCapKhenThuong"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdCapKhenThuong);
            ViewData["IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd"] = new SelectList(_context.DmThiDuaGiaiThuongKhenThuongs, "IdThiDuaGiaiThuongKhenThuong", "IdThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
            ViewData["IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmLoaiDanhHieuThiDuaGiaiThuongKhenThuongs, "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong);
            ViewData["IdPhuongThucKhenThuong"] = new SelectList(_context.DmPhuongThucKhenThuongs, "IdPhuongThucKhenThuong", "IdPhuongThucKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd.IdPhuongThucKhenThuong);
            return View(tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
        }

        // GET: DanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd = await _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGds
                .Include(t => t.IdCapKhenThuongNavigation)
                .Include(t => t.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGdNavigation)
                .Include(t => t.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuongNavigation)
                .Include(t => t.IdPhuongThucKhenThuongNavigation)
                .FirstOrDefaultAsync(m => m.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd == id);
            if (tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd == null)
            {
                return NotFound();
            }

            return View(tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
        }

        // POST: DanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd = await _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGds.FindAsync(id);
            if (tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd != null)
            {
                _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGds.Remove(tbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGdExists(int id)
        {
            return _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGds.Any(e => e.IdDanhHieuThiDuaGiaiThuongKhenThuongCuaCoSoGd == id);
        }
    }
}
