using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C500Hemis.Models;

namespace C500Hemis.Controllers.CB
{
    public class KyLuatCanBoController : Controller
    {
        private readonly HemisContext _context;

        public KyLuatCanBoController(HemisContext context)
        {
            _context = context;
        }

        // GET: KyLuatCanBo
        public async Task<IActionResult> Index()
        {
            var hemisContext = _context.TbKyLuatCanBos.Include(t => t.IdCanBoNavigation).Include(t => t.IdCapQuyetDinhNavigation).Include(t => t.IdLoaiKyLuatNavigation);
            return View(await hemisContext.ToListAsync());
        }

        // GET: KyLuatCanBo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbKyLuatCanBo = await _context.TbKyLuatCanBos
                .Include(t => t.IdCanBoNavigation)
                .Include(t => t.IdCapQuyetDinhNavigation)
                .Include(t => t.IdLoaiKyLuatNavigation)
                .FirstOrDefaultAsync(m => m.IdKyLuatCanBo == id);
            if (tbKyLuatCanBo == null)
            {
                return NotFound();
            }

            return View(tbKyLuatCanBo);
        }

        // GET: KyLuatCanBo/Create
        public IActionResult Create()
        {
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo");
            ViewData["IdCapQuyetDinh"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong");
            ViewData["IdLoaiKyLuat"] = new SelectList(_context.DmLoaiKyLuats, "IdLoaiKyLuat", "IdLoaiKyLuat");
            return View();
        }

        // POST: KyLuatCanBo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKyLuatCanBo,IdCanBo,IdLoaiKyLuat,LyDo,IdCapQuyetDinh,NgayThangNamQuyetDinh,SoQuyetDinh,NamBiKyLuat")] TbKyLuatCanBo tbKyLuatCanBo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbKyLuatCanBo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbKyLuatCanBo.IdCanBo);
            ViewData["IdCapQuyetDinh"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong", tbKyLuatCanBo.IdCapQuyetDinh);
            ViewData["IdLoaiKyLuat"] = new SelectList(_context.DmLoaiKyLuats, "IdLoaiKyLuat", "IdLoaiKyLuat", tbKyLuatCanBo.IdLoaiKyLuat);
            return View(tbKyLuatCanBo);
        }

        // GET: KyLuatCanBo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbKyLuatCanBo = await _context.TbKyLuatCanBos.FindAsync(id);
            if (tbKyLuatCanBo == null)
            {
                return NotFound();
            }
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbKyLuatCanBo.IdCanBo);
            ViewData["IdCapQuyetDinh"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong", tbKyLuatCanBo.IdCapQuyetDinh);
            ViewData["IdLoaiKyLuat"] = new SelectList(_context.DmLoaiKyLuats, "IdLoaiKyLuat", "IdLoaiKyLuat", tbKyLuatCanBo.IdLoaiKyLuat);
            return View(tbKyLuatCanBo);
        }

        // POST: KyLuatCanBo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKyLuatCanBo,IdCanBo,IdLoaiKyLuat,LyDo,IdCapQuyetDinh,NgayThangNamQuyetDinh,SoQuyetDinh,NamBiKyLuat")] TbKyLuatCanBo tbKyLuatCanBo)
        {
            if (id != tbKyLuatCanBo.IdKyLuatCanBo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbKyLuatCanBo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbKyLuatCanBoExists(tbKyLuatCanBo.IdKyLuatCanBo))
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
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbKyLuatCanBo.IdCanBo);
            ViewData["IdCapQuyetDinh"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong", tbKyLuatCanBo.IdCapQuyetDinh);
            ViewData["IdLoaiKyLuat"] = new SelectList(_context.DmLoaiKyLuats, "IdLoaiKyLuat", "IdLoaiKyLuat", tbKyLuatCanBo.IdLoaiKyLuat);
            return View(tbKyLuatCanBo);
        }

        // GET: KyLuatCanBo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbKyLuatCanBo = await _context.TbKyLuatCanBos
                .Include(t => t.IdCanBoNavigation)
                .Include(t => t.IdCapQuyetDinhNavigation)
                .Include(t => t.IdLoaiKyLuatNavigation)
                .FirstOrDefaultAsync(m => m.IdKyLuatCanBo == id);
            if (tbKyLuatCanBo == null)
            {
                return NotFound();
            }

            return View(tbKyLuatCanBo);
        }

        // POST: KyLuatCanBo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbKyLuatCanBo = await _context.TbKyLuatCanBos.FindAsync(id);
            if (tbKyLuatCanBo != null)
            {
                _context.TbKyLuatCanBos.Remove(tbKyLuatCanBo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbKyLuatCanBoExists(int id)
        {
            return _context.TbKyLuatCanBos.Any(e => e.IdKyLuatCanBo == id);
        }
    }
}
