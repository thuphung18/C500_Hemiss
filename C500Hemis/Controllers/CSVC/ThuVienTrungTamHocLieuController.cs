using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C500Hemis.Models;

namespace C500Hemis.Controllers.CSVC
{
    public class ThuVienTrungTamHocLieuController : Controller
    {
        private readonly HemisContext _context;

        public ThuVienTrungTamHocLieuController(HemisContext context)
        {
            _context = context;
        }

        // GET: ThuVienTrungTamHocLieu
        public async Task<IActionResult> Index(string searchString)
        {
            try // Xử lý bắt lỗi
            {
                // Lấy danh sách tất cả các thư viện
                var hemisContext = from s in _context.TbThuVienTrungTamHocLieus
                        .Include(t => t.IdHinhThucSoHuuNavigation)
                        .Include(t => t.IdTinhTrangCsvcNavigation)
                                   select s;
                // Nếu có giá trị tìm kiếm, lọc theo Tên Thư Viện
                if (!string.IsNullOrEmpty(searchString))
                {
                    hemisContext = hemisContext.Where(s => s.TenThuVienTrungTamHocLieu.Contains(searchString));
                }
                // Đếm tổng số bản ghi sau khi lọc
                ViewBag.TotalRecords = await hemisContext.CountAsync();

                // Trả về view với danh sách đã lọc
                return View(await hemisContext.ToListAsync());
            }
            catch (Exception ex)
            {
                // Log lỗi ra để biết thêm thông tin chi tiết nếu cần
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        // GET: ThuVienTrungTamHocLieu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThuVienTrungTamHocLieu = await _context.TbThuVienTrungTamHocLieus
                .Include(t => t.IdHinhThucSoHuuNavigation)
                .Include(t => t.IdTinhTrangCsvcNavigation)
                .FirstOrDefaultAsync(m => m.IdThuVienTrungTamHocLieu == id);
            if (tbThuVienTrungTamHocLieu == null)
            {
                return NotFound();
            }

            return View(tbThuVienTrungTamHocLieu);
        }

        // GET: ThuVienTrungTamHocLieu/Create
        public IActionResult Create()
        {
            ViewData["IdHinhThucSoHuu"] = new SelectList(_context.DmHinhThucSoHuus, "IdHinhThucSoHuu", "IdHinhThucSoHuu");
            ViewData["IdTinhTrangCsvc"] = new SelectList(_context.DmTinhTrangCoSoVatChats, "IdTinhTrangCoSoVatChat", "IdTinhTrangCoSoVatChat");
            return View();
        }

        // POST: ThuVienTrungTamHocLieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdThuVienTrungTamHocLieu,TenThuVienTrungTamHocLieu,NamDuaVaoSuDung,DienTich,...")] TbThuVienTrungTamHocLieu tbThuVienTrungTamHocLieu)
        {
            try
            { // xử lý bắt lỗi
                // Kiểm tra tên thư viện đã tồn tại chưa
                bool isDuplicate = await _context.TbThuVienTrungTamHocLieus
                    .AnyAsync(tv => tv.TenThuVienTrungTamHocLieu == tbThuVienTrungTamHocLieu.TenThuVienTrungTamHocLieu);

                if (isDuplicate)
                {
                    ModelState.AddModelError("TenThuVienTrungTamHocLieu", "Tên thư viện đã tồn tại.");
                    return View(tbThuVienTrungTamHocLieu);
                }

                if (ModelState.IsValid)
                {
                    _context.Add(tbThuVienTrungTamHocLieu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi tạo thư viện mới.");
            }
            ViewData["IdHinhThucSoHuu"] = new SelectList(_context.DmHinhThucSoHuus, "IdHinhThucSoHuu", "IdHinhThucSoHuu", tbThuVienTrungTamHocLieu.IdHinhThucSoHuu);
            ViewData["IdTinhTrangCsvc"] = new SelectList(_context.DmTinhTrangCoSoVatChats, "IdTinhTrangCoSoVatChat", "IdTinhTrangCoSoVatChat", tbThuVienTrungTamHocLieu.IdTinhTrangCsvc);
            return View(tbThuVienTrungTamHocLieu);
        }

        // GET: ThuVienTrungTamHocLieu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThuVienTrungTamHocLieu = await _context.TbThuVienTrungTamHocLieus.FindAsync(id);
            if (tbThuVienTrungTamHocLieu == null)
            {
                return NotFound();
            }
            ViewData["IdHinhThucSoHuu"] = new SelectList(_context.DmHinhThucSoHuus, "IdHinhThucSoHuu", "IdHinhThucSoHuu", tbThuVienTrungTamHocLieu.IdHinhThucSoHuu);
            ViewData["IdTinhTrangCsvc"] = new SelectList(_context.DmTinhTrangCoSoVatChats, "IdTinhTrangCoSoVatChat", "IdTinhTrangCoSoVatChat", tbThuVienTrungTamHocLieu.IdTinhTrangCsvc);
            return View(tbThuVienTrungTamHocLieu);
        }

        // POST: ThuVienTrungTamHocLieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdThuVienTrungTamHocLieu,TenThuVienTrungTamHocLieu,NamDuaVaoSuDung,DienTich,DienTichPhongDoc,SoPhongDoc,SoLuongMayTinh,SoLuongChoNgoi,SoLuongSach,SoLuongTapChi,SoLuongSachDienTu,SoLuongTapChiDienTu,SoLuonngThuVienDienTuLienKetNn,SoLuongDauSach,SoLuongDauTapChi,SoLuongDauSachDienTu,SoLuongDauTapChiDienTu,IdHinhThucSoHuu,IdTinhTrangCsvc")] TbThuVienTrungTamHocLieu tbThuVienTrungTamHocLieu)
        {
            if (id != tbThuVienTrungTamHocLieu.IdThuVienTrungTamHocLieu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbThuVienTrungTamHocLieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbThuVienTrungTamHocLieuExists(tbThuVienTrungTamHocLieu.IdThuVienTrungTamHocLieu))
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
            ViewData["IdHinhThucSoHuu"] = new SelectList(_context.DmHinhThucSoHuus, "IdHinhThucSoHuu", "IdHinhThucSoHuu", tbThuVienTrungTamHocLieu.IdHinhThucSoHuu);
            ViewData["IdTinhTrangCsvc"] = new SelectList(_context.DmTinhTrangCoSoVatChats, "IdTinhTrangCoSoVatChat", "IdTinhTrangCoSoVatChat", tbThuVienTrungTamHocLieu.IdTinhTrangCsvc);
            return View(tbThuVienTrungTamHocLieu);
        }

        // GET: ThuVienTrungTamHocLieu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThuVienTrungTamHocLieu = await _context.TbThuVienTrungTamHocLieus
                .Include(t => t.IdHinhThucSoHuuNavigation)
                .Include(t => t.IdTinhTrangCsvcNavigation)
                .FirstOrDefaultAsync(m => m.IdThuVienTrungTamHocLieu == id);
            if (tbThuVienTrungTamHocLieu == null)
            {
                return NotFound();
            }

            return View(tbThuVienTrungTamHocLieu);
        }

        // POST: ThuVienTrungTamHocLieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbThuVienTrungTamHocLieu = await _context.TbThuVienTrungTamHocLieus.FindAsync(id);
            if (tbThuVienTrungTamHocLieu != null)
            {
                _context.TbThuVienTrungTamHocLieus.Remove(tbThuVienTrungTamHocLieu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbThuVienTrungTamHocLieuExists(int id)
        {
            return _context.TbThuVienTrungTamHocLieus.Any(e => e.IdThuVienTrungTamHocLieu == id);
        }
    }
}
