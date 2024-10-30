using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C500Hemis.Models;

namespace C500Hemis.Controllers.HTQT
{
    public class TbThongTinHopTacsController : Controller
    {
        private readonly HemisContext _context;

        public TbThongTinHopTacsController(HemisContext context)
        {
            _context = context;
        }

        // GET: TbThongTinHopTacs
        public async Task<IActionResult> Index()
        {
            var hemisContext = _context.TbThongTinHopTacs
            .Include(t => t.IdHinhThucHopTacNavigation)
            .Include(t => t.IdToChucHopTacNavigation);
            return View(await hemisContext.ToListAsync());
        }

        //Chức năng tìm kiếm
        [HttpGet("index-search")]
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                // Lấy toàn bộ dữ liệu từ bảng
                var query = _context.TbThongTinHopTacs
                    .Include(t => t.IdHinhThucHopTacNavigation)
                    .Include(t => t.IdToChucHopTacNavigation)
                   
                    .AsQueryable(); // Chuyển đổi thành IQueryable để có thể lọc dữ liệu

                // Nếu có tham số tìm kiếm, lọc dữ liệu theo từ khóa
                if (!string.IsNullOrEmpty(searchString))
                {
                    query = query.Where(t => t.TenThoaThuan.Contains(searchString) ||
                                             t.ThongTinLienHeDoiTac.Contains(searchString) ||
                                             t.IdThongTinHopTac.ToString().Contains(searchString));
                }

                ViewData["CurrentFilter"] = searchString; // Truyền lại từ khóa tìm kiếm cho View
                return View(await query.ToListAsync()); // Trả về kết quả sau khi lọc
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }







        // GET: TbThongTinHopTacs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThongTinHopTac = await _context.TbThongTinHopTacs
                .Include(t => t.IdHinhThucHopTacNavigation)
                .Include(t => t.IdToChucHopTacNavigation)
                .FirstOrDefaultAsync(m => m.IdThongTinHopTac == id);
            if (tbThongTinHopTac == null)
            {
                return NotFound();
            }

            return View(tbThongTinHopTac);
        }

        // GET: TbThongTinHopTacs/Create
        public IActionResult Create()
        {
            ViewData["IdHinhThucHopTac"] = new SelectList(_context.DmHinhThucHopTacs, "IdHinhThucHopTac", "IdHinhThucHopTac");
            ViewData["IdToChucHopTac"] = new SelectList(_context.TbToChucHopTacQuocTes, "IdToChucHopTacQuocTe", "IdToChucHopTacQuocTe");
            return View();
        }

        // POST: TbThongTinHopTacs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdThongTinHopTac,IdToChucHopTac,ThoiGianHopTacTu,ThoiGianHopTacDen,TenThoaThuan,ThongTinLienHeDoiTac,MucTieu,DonViTrienKhai,IdHinhThucHopTac,SanPhamChinh")] TbThongTinHopTac tbThongTinHopTac)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra nếu IdThongTinHopTac đã tồn tại trong bảng
                    if (await _context.TbThongTinHopTacs.AnyAsync(t => t.IdThongTinHopTac == tbThongTinHopTac.IdThongTinHopTac))
                    {
                        // Nếu có bản ghi trùng lặp, thêm thông báo lỗi vào ModelState
                        ModelState.AddModelError("IdThongTinHopTac", "Đã có bản ghi với Id này. Vui lòng nhập Id khác.");
                    }
                    else
                    {
                        // Kiểm tra nếu IdToChucHopTac đã tồn tại trong bảng TbToChucHopTacQuocTe
                        var existingToChuc = await _context.TbToChucHopTacQuocTes
                                              .FirstOrDefaultAsync(t => t.IdToChucHopTacQuocTe == tbThongTinHopTac.IdToChucHopTac);

                        // Nếu không tồn tại, thêm tổ chức mới vào TbToChucHopTacQuocTe
                        if (existingToChuc == null)
                        {
                            var newToChuc = new TbToChucHopTacQuocTe
                            {
                                IdToChucHopTacQuocTe = (int)tbThongTinHopTac.IdToChucHopTac,
                            };
                            _context.TbToChucHopTacQuocTes.Add(newToChuc);
                            await _context.SaveChangesAsync();
                        }

                        // Sau đó thêm dữ liệu vào bảng TbThongTinHopTac
                        _context.Add(tbThongTinHopTac);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateException ex)
                {
                    // Xử lý ngoại lệ liên quan đến việc cập nhật cơ sở dữ liệu
                    ModelState.AddModelError("", "Không thể thêm bản ghi vào cơ sở dữ liệu. Vui lòng thử lại.");
                  
                }
            }

            // Nếu có lỗi, hiển thị lại form với thông tin lỗi
            ViewData["IdHinhThucHopTac"] = new SelectList(_context.DmHinhThucHopTacs, "IdHinhThucHopTac", "IdHinhThucHopTac", tbThongTinHopTac.IdHinhThucHopTac);
            ViewData["IdToChucHopTac"] = new SelectList(_context.TbToChucHopTacQuocTes, "IdToChucHopTacQuocTe", "IdToChucHopTacQuocTe", tbThongTinHopTac.IdToChucHopTac);
            return View(tbThongTinHopTac);
        }

        // GET: TbThongTinHopTacs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThongTinHopTac = await _context.TbThongTinHopTacs.FindAsync(id);
            if (tbThongTinHopTac == null)
            {
                return NotFound();
            }
            ViewData["IdHinhThucHopTac"] = new SelectList(_context.DmHinhThucHopTacs, "IdHinhThucHopTac", "IdHinhThucHopTac", tbThongTinHopTac.IdHinhThucHopTac);
            ViewData["IdToChucHopTac"] = new SelectList(_context.TbToChucHopTacQuocTes, "IdToChucHopTacQuocTe", "IdToChucHopTacQuocTe", tbThongTinHopTac.IdToChucHopTac);
            return View(tbThongTinHopTac);
        }

        // POST: TbThongTinHopTacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdThongTinHopTac,IdToChucHopTac,ThoiGianHopTacTu,ThoiGianHopTacDen,TenThoaThuan,ThongTinLienHeDoiTac,MucTieu,DonViTrienKhai,IdHinhThucHopTac,SanPhamChinh")] TbThongTinHopTac tbThongTinHopTac)
        {
            if (id != tbThongTinHopTac.IdThongTinHopTac)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbThongTinHopTac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbThongTinHopTacExists(tbThongTinHopTac.IdThongTinHopTac))
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
            ViewData["IdHinhThucHopTac"] = new SelectList(_context.DmHinhThucHopTacs, "IdHinhThucHopTac", "IdHinhThucHopTac", tbThongTinHopTac.IdHinhThucHopTac);
            ViewData["IdToChucHopTac"] = new SelectList(_context.TbToChucHopTacQuocTes, "IdToChucHopTacQuocTe", "IdToChucHopTacQuocTe", tbThongTinHopTac.IdToChucHopTac);
            return View(tbThongTinHopTac);
        }

        // GET: TbThongTinHopTacs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThongTinHopTac = await _context.TbThongTinHopTacs
                .Include(t => t.IdHinhThucHopTacNavigation)
                .Include(t => t.IdToChucHopTacNavigation)
                .FirstOrDefaultAsync(m => m.IdThongTinHopTac == id);
            if (tbThongTinHopTac == null)
            {
                return NotFound();
            }

            return View(tbThongTinHopTac);
        }

        // POST: TbThongTinHopTacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbThongTinHopTac = await _context.TbThongTinHopTacs.FindAsync(id);
            if (tbThongTinHopTac != null)
            {
                _context.TbThongTinHopTacs.Remove(tbThongTinHopTac);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbThongTinHopTacExists(int id)
        {
            return _context.TbThongTinHopTacs.Any(e => e.IdThongTinHopTac == id);
        }
    }
}
