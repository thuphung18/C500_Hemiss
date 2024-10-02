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
    public class QuaTrinhCongTacCuaCanBoController : Controller
    {
        private readonly HemisContext _context;

        public QuaTrinhCongTacCuaCanBoController(HemisContext context)
        {
            _context = context;
        }

        // GET: QuaTrinhCongTacCuaCanBo
        public async Task<IActionResult> Index()
        {
            var hemisContext = _context.TbQuaTrinhCongTacCuaCanBos.Include(t => t.IdCanBoNavigation).Include(t => t.IdChucDanhGiangVienNavigation).Include(t => t.IdChucVuNavigation);
            return View(await hemisContext.ToListAsync());
        }

        // GET: QuaTrinhCongTacCuaCanBo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuaTrinhCongTacCuaCanBo = await _context.TbQuaTrinhCongTacCuaCanBos
                .Include(t => t.IdCanBoNavigation)
                .Include(t => t.IdChucDanhGiangVienNavigation)
                .Include(t => t.IdChucVuNavigation)
                .FirstOrDefaultAsync(m => m.IdQuaTrinhCongTacCuaCanBo == id);
            if (tbQuaTrinhCongTacCuaCanBo == null)
            {
                return NotFound();
            }

            return View(tbQuaTrinhCongTacCuaCanBo);
        }

        // GET: QuaTrinhCongTacCuaCanBo/Create
        public IActionResult Create()
        {
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo");
            ViewData["IdChucDanhGiangVien"] = new SelectList(_context.DmChucDanhGiangViens, "IdChucDanhGiangVien", "IdChucDanhGiangVien");
            ViewData["IdChucVu"] = new SelectList(_context.DmChucVus, "IdChucVu", "IdChucVu");
            return View();
        }

        // POST: QuaTrinhCongTacCuaCanBo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdQuaTrinhCongTacCuaCanBo,IdCanBo,TuThangNam,DenThangNam,IdChucVu,IdChucDanhGiangVien,DonViCongTac")] TbQuaTrinhCongTacCuaCanBo tbQuaTrinhCongTacCuaCanBo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbQuaTrinhCongTacCuaCanBo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbQuaTrinhCongTacCuaCanBo.IdCanBo);
            ViewData["IdChucDanhGiangVien"] = new SelectList(_context.DmChucDanhGiangViens, "IdChucDanhGiangVien", "IdChucDanhGiangVien", tbQuaTrinhCongTacCuaCanBo.IdChucDanhGiangVien);
            ViewData["IdChucVu"] = new SelectList(_context.DmChucVus, "IdChucVu", "IdChucVu", tbQuaTrinhCongTacCuaCanBo.IdChucVu);
            return View(tbQuaTrinhCongTacCuaCanBo);
        }

        // GET: QuaTrinhCongTacCuaCanBo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuaTrinhCongTacCuaCanBo = await _context.TbQuaTrinhCongTacCuaCanBos.FindAsync(id);
            if (tbQuaTrinhCongTacCuaCanBo == null)
            {
                return NotFound();
            }
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbQuaTrinhCongTacCuaCanBo.IdCanBo);
            ViewData["IdChucDanhGiangVien"] = new SelectList(_context.DmChucDanhGiangViens, "IdChucDanhGiangVien", "IdChucDanhGiangVien", tbQuaTrinhCongTacCuaCanBo.IdChucDanhGiangVien);
            ViewData["IdChucVu"] = new SelectList(_context.DmChucVus, "IdChucVu", "IdChucVu", tbQuaTrinhCongTacCuaCanBo.IdChucVu);
            return View(tbQuaTrinhCongTacCuaCanBo);
        }

        // POST: QuaTrinhCongTacCuaCanBo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdQuaTrinhCongTacCuaCanBo,IdCanBo,TuThangNam,DenThangNam,IdChucVu,IdChucDanhGiangVien,DonViCongTac")] TbQuaTrinhCongTacCuaCanBo tbQuaTrinhCongTacCuaCanBo)
        {
            if (id != tbQuaTrinhCongTacCuaCanBo.IdQuaTrinhCongTacCuaCanBo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbQuaTrinhCongTacCuaCanBo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbQuaTrinhCongTacCuaCanBoExists(tbQuaTrinhCongTacCuaCanBo.IdQuaTrinhCongTacCuaCanBo))
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
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbQuaTrinhCongTacCuaCanBo.IdCanBo);
            ViewData["IdChucDanhGiangVien"] = new SelectList(_context.DmChucDanhGiangViens, "IdChucDanhGiangVien", "IdChucDanhGiangVien", tbQuaTrinhCongTacCuaCanBo.IdChucDanhGiangVien);
            ViewData["IdChucVu"] = new SelectList(_context.DmChucVus, "IdChucVu", "IdChucVu", tbQuaTrinhCongTacCuaCanBo.IdChucVu);
            return View(tbQuaTrinhCongTacCuaCanBo);
        }

        // GET: QuaTrinhCongTacCuaCanBo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuaTrinhCongTacCuaCanBo = await _context.TbQuaTrinhCongTacCuaCanBos
                .Include(t => t.IdCanBoNavigation)
                .Include(t => t.IdChucDanhGiangVienNavigation)
                .Include(t => t.IdChucVuNavigation)
                .FirstOrDefaultAsync(m => m.IdQuaTrinhCongTacCuaCanBo == id);
            if (tbQuaTrinhCongTacCuaCanBo == null)
            {
                return NotFound();
            }

            return View(tbQuaTrinhCongTacCuaCanBo);
        }

        // POST: QuaTrinhCongTacCuaCanBo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbQuaTrinhCongTacCuaCanBo = await _context.TbQuaTrinhCongTacCuaCanBos.FindAsync(id);
            if (tbQuaTrinhCongTacCuaCanBo != null)
            {
                _context.TbQuaTrinhCongTacCuaCanBos.Remove(tbQuaTrinhCongTacCuaCanBo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbQuaTrinhCongTacCuaCanBoExists(int id)
        {
            return _context.TbQuaTrinhCongTacCuaCanBos.Any(e => e.IdQuaTrinhCongTacCuaCanBo == id);
        }
    }
}
