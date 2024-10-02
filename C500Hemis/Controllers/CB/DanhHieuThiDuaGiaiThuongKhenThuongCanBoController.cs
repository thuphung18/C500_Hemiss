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
    public class DanhHieuThiDuaGiaiThuongKhenThuongCanBoController : Controller
    {
        private readonly HemisContext _context;

        public DanhHieuThiDuaGiaiThuongKhenThuongCanBoController(HemisContext context)
        {
            _context = context;
        }

        // GET: DanhHieuThiDuaGiaiThuongKhenThuongCanBo
        public async Task<IActionResult> Index()
        {
            var hemisContext = _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCanBos.Include(t => t.IdCanBoNavigation).Include(t => t.IdCapKhenThuongNavigation).Include(t => t.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuongNavigation).Include(t => t.IdPhuongThucKhenThuongNavigation).Include(t => t.IdThiDuaGiaiThuongKhenThuongNavigation);
            return View(await hemisContext.ToListAsync());
        }

        // GET: DanhHieuThiDuaGiaiThuongKhenThuongCanBo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo = await _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCanBos
                .Include(t => t.IdCanBoNavigation)
                .Include(t => t.IdCapKhenThuongNavigation)
                .Include(t => t.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuongNavigation)
                .Include(t => t.IdPhuongThucKhenThuongNavigation)
                .Include(t => t.IdThiDuaGiaiThuongKhenThuongNavigation)
                .FirstOrDefaultAsync(m => m.IdDanhHieuThiDuaGiaiThuongKhenThuongCanBo == id);
            if (tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo == null)
            {
                return NotFound();
            }

            return View(tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo);
        }

        // GET: DanhHieuThiDuaGiaiThuongKhenThuongCanBo/Create
        public IActionResult Create()
        {
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo");
            ViewData["IdCapKhenThuong"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong");
            ViewData["IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmLoaiDanhHieuThiDuaGiaiThuongKhenThuongs, "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong");
            ViewData["IdPhuongThucKhenThuong"] = new SelectList(_context.DmPhuongThucKhenThuongs, "IdPhuongThucKhenThuong", "IdPhuongThucKhenThuong");
            ViewData["IdThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmThiDuaGiaiThuongKhenThuongs, "IdThiDuaGiaiThuongKhenThuong", "IdThiDuaGiaiThuongKhenThuong");
            return View();
        }

        // POST: DanhHieuThiDuaGiaiThuongKhenThuongCanBo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDanhHieuThiDuaGiaiThuongKhenThuongCanBo,IdCanBo,IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong,IdThiDuaGiaiThuongKhenThuong,SoQuyetDinh,IdPhuongThucKhenThuong,NamKhenThuong,IdCapKhenThuong")] TbDanhHieuThiDuaGiaiThuongKhenThuongCanBo tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdCanBo);
            ViewData["IdCapKhenThuong"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdCapKhenThuong);
            ViewData["IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmLoaiDanhHieuThiDuaGiaiThuongKhenThuongs, "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong);
            ViewData["IdPhuongThucKhenThuong"] = new SelectList(_context.DmPhuongThucKhenThuongs, "IdPhuongThucKhenThuong", "IdPhuongThucKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdPhuongThucKhenThuong);
            ViewData["IdThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmThiDuaGiaiThuongKhenThuongs, "IdThiDuaGiaiThuongKhenThuong", "IdThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdThiDuaGiaiThuongKhenThuong);
            return View(tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo);
        }

        // GET: DanhHieuThiDuaGiaiThuongKhenThuongCanBo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo = await _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCanBos.FindAsync(id);
            if (tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo == null)
            {
                return NotFound();
            }
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdCanBo);
            ViewData["IdCapKhenThuong"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdCapKhenThuong);
            ViewData["IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmLoaiDanhHieuThiDuaGiaiThuongKhenThuongs, "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong);
            ViewData["IdPhuongThucKhenThuong"] = new SelectList(_context.DmPhuongThucKhenThuongs, "IdPhuongThucKhenThuong", "IdPhuongThucKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdPhuongThucKhenThuong);
            ViewData["IdThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmThiDuaGiaiThuongKhenThuongs, "IdThiDuaGiaiThuongKhenThuong", "IdThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdThiDuaGiaiThuongKhenThuong);
            return View(tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo);
        }

        // POST: DanhHieuThiDuaGiaiThuongKhenThuongCanBo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDanhHieuThiDuaGiaiThuongKhenThuongCanBo,IdCanBo,IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong,IdThiDuaGiaiThuongKhenThuong,SoQuyetDinh,IdPhuongThucKhenThuong,NamKhenThuong,IdCapKhenThuong")] TbDanhHieuThiDuaGiaiThuongKhenThuongCanBo tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo)
        {
            if (id != tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdDanhHieuThiDuaGiaiThuongKhenThuongCanBo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbDanhHieuThiDuaGiaiThuongKhenThuongCanBoExists(tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdDanhHieuThiDuaGiaiThuongKhenThuongCanBo))
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
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdCanBo);
            ViewData["IdCapKhenThuong"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "IdCapKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdCapKhenThuong);
            ViewData["IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmLoaiDanhHieuThiDuaGiaiThuongKhenThuongs, "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", "IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuong);
            ViewData["IdPhuongThucKhenThuong"] = new SelectList(_context.DmPhuongThucKhenThuongs, "IdPhuongThucKhenThuong", "IdPhuongThucKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdPhuongThucKhenThuong);
            ViewData["IdThiDuaGiaiThuongKhenThuong"] = new SelectList(_context.DmThiDuaGiaiThuongKhenThuongs, "IdThiDuaGiaiThuongKhenThuong", "IdThiDuaGiaiThuongKhenThuong", tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo.IdThiDuaGiaiThuongKhenThuong);
            return View(tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo);
        }

        // GET: DanhHieuThiDuaGiaiThuongKhenThuongCanBo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo = await _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCanBos
                .Include(t => t.IdCanBoNavigation)
                .Include(t => t.IdCapKhenThuongNavigation)
                .Include(t => t.IdLoaiDanhHieuThiDuaGiaiThuongKhenThuongNavigation)
                .Include(t => t.IdPhuongThucKhenThuongNavigation)
                .Include(t => t.IdThiDuaGiaiThuongKhenThuongNavigation)
                .FirstOrDefaultAsync(m => m.IdDanhHieuThiDuaGiaiThuongKhenThuongCanBo == id);
            if (tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo == null)
            {
                return NotFound();
            }

            return View(tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo);
        }

        // POST: DanhHieuThiDuaGiaiThuongKhenThuongCanBo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo = await _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCanBos.FindAsync(id);
            if (tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo != null)
            {
                _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCanBos.Remove(tbDanhHieuThiDuaGiaiThuongKhenThuongCanBo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbDanhHieuThiDuaGiaiThuongKhenThuongCanBoExists(int id)
        {
            return _context.TbDanhHieuThiDuaGiaiThuongKhenThuongCanBos.Any(e => e.IdDanhHieuThiDuaGiaiThuongKhenThuongCanBo == id);
        }
    }
}
