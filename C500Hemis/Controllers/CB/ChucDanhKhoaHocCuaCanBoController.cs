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
    public class ChucDanhKhoaHocCuaCanBoController : Controller
    {
        private readonly HemisContext _context;

        public ChucDanhKhoaHocCuaCanBoController(HemisContext context)
        {
            _context = context;
        }

        // GET: ChucDanhKhoaHocCuaCanBo
        public async Task<IActionResult> Index()
        {
            var hemisContext = _context.TbChucDanhKhoaHocCuaCanBos.Include(t => t.IdCanBoNavigation).Include(t => t.IdChucDanhKhoaHocNavigation).Include(t => t.IdThamQuyenQuyetDinhNavigation);
            return View(await hemisContext.ToListAsync());
        }

        // GET: ChucDanhKhoaHocCuaCanBo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbChucDanhKhoaHocCuaCanBo = await _context.TbChucDanhKhoaHocCuaCanBos
                .Include(t => t.IdCanBoNavigation)
                .Include(t => t.IdChucDanhKhoaHocNavigation)
                .Include(t => t.IdThamQuyenQuyetDinhNavigation)
                .FirstOrDefaultAsync(m => m.IdChucDanhKhoaHocCuaCanBo == id);
            if (tbChucDanhKhoaHocCuaCanBo == null)
            {
                return NotFound();
            }

            return View(tbChucDanhKhoaHocCuaCanBo);
        }

        // GET: ChucDanhKhoaHocCuaCanBo/Create
        public IActionResult Create()
        {
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo");
            ViewData["IdChucDanhKhoaHoc"] = new SelectList(_context.DmChucDanhKhoaHocs, "IdChucDanhKhoaHoc", "IdChucDanhKhoaHoc");
            ViewData["IdThamQuyenQuyetDinh"] = new SelectList(_context.DmLoaiQuyetDinhs, "IdLoaiQuyetDinh", "IdLoaiQuyetDinh");
            return View();
        }

        // POST: ChucDanhKhoaHocCuaCanBo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdChucDanhKhoaHocCuaCanBo,IdCanBo,IdChucDanhKhoaHoc,IdThamQuyenQuyetDinh,SoQuyetDinh,NgayQuyetDinh")] TbChucDanhKhoaHocCuaCanBo tbChucDanhKhoaHocCuaCanBo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbChucDanhKhoaHocCuaCanBo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbChucDanhKhoaHocCuaCanBo.IdCanBo);
            ViewData["IdChucDanhKhoaHoc"] = new SelectList(_context.DmChucDanhKhoaHocs, "IdChucDanhKhoaHoc", "IdChucDanhKhoaHoc", tbChucDanhKhoaHocCuaCanBo.IdChucDanhKhoaHoc);
            ViewData["IdThamQuyenQuyetDinh"] = new SelectList(_context.DmLoaiQuyetDinhs, "IdLoaiQuyetDinh", "IdLoaiQuyetDinh", tbChucDanhKhoaHocCuaCanBo.IdThamQuyenQuyetDinh);
            return View(tbChucDanhKhoaHocCuaCanBo);
        }

        // GET: ChucDanhKhoaHocCuaCanBo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbChucDanhKhoaHocCuaCanBo = await _context.TbChucDanhKhoaHocCuaCanBos.FindAsync(id);
            if (tbChucDanhKhoaHocCuaCanBo == null)
            {
                return NotFound();
            }
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbChucDanhKhoaHocCuaCanBo.IdCanBo);
            ViewData["IdChucDanhKhoaHoc"] = new SelectList(_context.DmChucDanhKhoaHocs, "IdChucDanhKhoaHoc", "IdChucDanhKhoaHoc", tbChucDanhKhoaHocCuaCanBo.IdChucDanhKhoaHoc);
            ViewData["IdThamQuyenQuyetDinh"] = new SelectList(_context.DmLoaiQuyetDinhs, "IdLoaiQuyetDinh", "IdLoaiQuyetDinh", tbChucDanhKhoaHocCuaCanBo.IdThamQuyenQuyetDinh);
            return View(tbChucDanhKhoaHocCuaCanBo);
        }

        // POST: ChucDanhKhoaHocCuaCanBo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdChucDanhKhoaHocCuaCanBo,IdCanBo,IdChucDanhKhoaHoc,IdThamQuyenQuyetDinh,SoQuyetDinh,NgayQuyetDinh")] TbChucDanhKhoaHocCuaCanBo tbChucDanhKhoaHocCuaCanBo)
        {
            if (id != tbChucDanhKhoaHocCuaCanBo.IdChucDanhKhoaHocCuaCanBo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbChucDanhKhoaHocCuaCanBo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbChucDanhKhoaHocCuaCanBoExists(tbChucDanhKhoaHocCuaCanBo.IdChucDanhKhoaHocCuaCanBo))
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
            ViewData["IdCanBo"] = new SelectList(_context.TbCanBos, "IdCanBo", "IdCanBo", tbChucDanhKhoaHocCuaCanBo.IdCanBo);
            ViewData["IdChucDanhKhoaHoc"] = new SelectList(_context.DmChucDanhKhoaHocs, "IdChucDanhKhoaHoc", "IdChucDanhKhoaHoc", tbChucDanhKhoaHocCuaCanBo.IdChucDanhKhoaHoc);
            ViewData["IdThamQuyenQuyetDinh"] = new SelectList(_context.DmLoaiQuyetDinhs, "IdLoaiQuyetDinh", "IdLoaiQuyetDinh", tbChucDanhKhoaHocCuaCanBo.IdThamQuyenQuyetDinh);
            return View(tbChucDanhKhoaHocCuaCanBo);
        }

        // GET: ChucDanhKhoaHocCuaCanBo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbChucDanhKhoaHocCuaCanBo = await _context.TbChucDanhKhoaHocCuaCanBos
                .Include(t => t.IdCanBoNavigation)
                .Include(t => t.IdChucDanhKhoaHocNavigation)
                .Include(t => t.IdThamQuyenQuyetDinhNavigation)
                .FirstOrDefaultAsync(m => m.IdChucDanhKhoaHocCuaCanBo == id);
            if (tbChucDanhKhoaHocCuaCanBo == null)
            {
                return NotFound();
            }

            return View(tbChucDanhKhoaHocCuaCanBo);
        }

        // POST: ChucDanhKhoaHocCuaCanBo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbChucDanhKhoaHocCuaCanBo = await _context.TbChucDanhKhoaHocCuaCanBos.FindAsync(id);
            if (tbChucDanhKhoaHocCuaCanBo != null)
            {
                _context.TbChucDanhKhoaHocCuaCanBos.Remove(tbChucDanhKhoaHocCuaCanBo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbChucDanhKhoaHocCuaCanBoExists(int id)
        {
            return _context.TbChucDanhKhoaHocCuaCanBos.Any(e => e.IdChucDanhKhoaHocCuaCanBo == id);
        }
    }
}
