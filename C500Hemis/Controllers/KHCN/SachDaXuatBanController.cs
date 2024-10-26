using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C500Hemis.Models;

namespace C500Hemis.Controllers.KHCN
{
    public class SachDaXuatBanController : Controller
    {
        private readonly HemisContext _context;

        public SachDaXuatBanController(HemisContext context)
        {
            _context = context;
        }

        // GET: SachDaXuatBan
        public async Task<IActionResult> Index(string SapXep, string TS)
        {
            // Gán tên sách tìm kiếm vào ViewBag để hiển thị trong view
            ViewBag.TenSach = TS;

            // Khởi tạo đối tượng db từ HemisContext để truy cập dữ liệu
            HemisContext db = new HemisContext();

            // Lấy toàn bộ danh sách sách đã xuất bản từ cơ sở dữ liệu
            var kq = db.TbSachDaXuatBans.ToList();

            // Khai báo biến danhSach để chứa các sách đã xuất bản với thông tin chi tiết
            var danhSach = db.TbSachDaXuatBans
                .Include(t => t.IdDangTaiLieuNavigation) // Bao gồm thông tin dạng tài liệu
                .Include(t => t.IdLoaiSachTapChiNavigation) // Bao gồm thông tin loại sách/tạp chí
                .Include(t => t.IdNhiemVuKhcnNavigation) // Bao gồm thông tin nhiệm vụ KHCN
                .Where(t => string.IsNullOrEmpty(TS) || t.TenSach.ToString() == TS) // Lọc theo tên sách nếu có
                .ToList();

            // Khai báo biến sxds để lưu trữ danh sách sách đã xuất bản
            var sxds = danhSach;

            // Kiểm tra xem có yêu cầu sắp xếp hay không
            if (SapXep == "SapXep")
            {
                // Nếu có yêu cầu sắp xếp, sắp xếp theo năm xuất bản
                sxds = danhSach.OrderBy(x => x.NamXuatBan).ToList();
            }

            // Lưu kết quả tìm kiếm vào ViewBag để sử dụng trong view
            ViewBag.KqTimKiem = danhSach;

            // Lưu kết quả đã sắp xếp vào ViewBag để sử dụng trong view
            ViewBag.KqqSapXep = sxds;

            // Trả về view với danh sách sách đã được sắp xếp (hoặc không)
            return View(sxds);
        }



        // GET: SachDaXuatBan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSachDaXuatBan = await _context.TbSachDaXuatBans
                .Include(t => t.IdDangTaiLieuNavigation)
                .Include(t => t.IdLoaiSachTapChiNavigation)
                .Include(t => t.IdNhiemVuKhcnNavigation)
                .FirstOrDefaultAsync(m => m.IdSachDaXuatBan == id);
            if (tbSachDaXuatBan == null)
            {
                return NotFound();
            }

            return View(tbSachDaXuatBan);
        }

        // GET: SachDaXuatBan/Create
        public IActionResult Create()
        {
            ViewData["IdDangTaiLieu"] = new SelectList(_context.DmDangTaiLieus, "IdDangTaiLieu", "DangTaiLieu");
            ViewData["IdLoaiSachTapChi"] = new SelectList(_context.DmLoaiSachTapChis, "IdLoaiSachTapChi", "LoaiSachTapChi");
            ViewData["IdNhiemVuKhcn"] = new SelectList(_context.TbNhiemVuKhcns, "IdNhiemVuKhcn", "IdNhiemVuKhcn");
            return View();
        }

        // POST: SachDaXuatBan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSachDaXuatBan,IdNhiemVuKhcn,MaSach,TenSach,IdLoaiSachTapChi,MaChuanIsbn,SoTrang,Nxb,NamXuatBan,NamViet,IdDangTaiLieu")] TbSachDaXuatBan tbSachDaXuatBan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbSachDaXuatBan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDangTaiLieu"] = new SelectList(_context.DmDangTaiLieus, "IdDangTaiLieu", "DangTaiLieu", tbSachDaXuatBan.IdDangTaiLieu);
            ViewData["IdLoaiSachTapChi"] = new SelectList(_context.DmLoaiSachTapChis, "IdLoaiSachTapChi", "LoaiSachTapChi", tbSachDaXuatBan.IdLoaiSachTapChi);
            ViewData["IdNhiemVuKhcn"] = new SelectList(_context.TbNhiemVuKhcns, "IdNhiemVuKhcn", "IdNhiemVuKhcn", tbSachDaXuatBan.IdNhiemVuKhcn);
            return View(tbSachDaXuatBan);
        }

        // GET: SachDaXuatBan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSachDaXuatBan = await _context.TbSachDaXuatBans.FindAsync(id);
            if (tbSachDaXuatBan == null)
            {
                return NotFound();
            }
            ViewData["IdDangTaiLieu"] = new SelectList(_context.DmDangTaiLieus, "IdDangTaiLieu", "DangTaiLieu", tbSachDaXuatBan.IdDangTaiLieu);
            ViewData["IdLoaiSachTapChi"] = new SelectList(_context.DmLoaiSachTapChis, "IdLoaiSachTapChi", "LoaiSachTapChi", tbSachDaXuatBan.IdLoaiSachTapChi);
            ViewData["IdNhiemVuKhcn"] = new SelectList(_context.TbNhiemVuKhcns, "IdNhiemVuKhcn", "IdNhiemVuKhcn", tbSachDaXuatBan.IdNhiemVuKhcn);
            return View(tbSachDaXuatBan);
        }

        // POST: SachDaXuatBan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSachDaXuatBan,IdNhiemVuKhcn,MaSach,TenSach,IdLoaiSachTapChi,MaChuanIsbn,SoTrang,Nxb,NamXuatBan,NamViet,IdDangTaiLieu")] TbSachDaXuatBan tbSachDaXuatBan)
        {
            // Kiểm tra xem ID của sách từ đường dẫn có khớp với ID trong đối tượng tbSachDaXuatBan không
            if (id != tbSachDaXuatBan.IdSachDaXuatBan)
            {
                // Nếu không khớp, trả về lỗi NotFound
                return NotFound();
            }

            // Kiểm tra xem model có hợp lệ hay không
            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật thông tin sách trong ngữ cảnh (context) của cơ sở dữ liệu
                    _context.Update(tbSachDaXuatBan);
                    // Lưu các thay đổi vào cơ sở dữ liệu
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Xử lý trường hợp có sự đồng thời cập nhật
                    // Kiểm tra xem sách có tồn tại không
                    if (!TbSachDaXuatBanExists(tbSachDaXuatBan.IdSachDaXuatBan))
                    {
                        // Nếu không tồn tại, trả về lỗi NotFound
                        return NotFound();
                    }
                    else
                    {
                        // Nếu có lỗi khác, ném lại ngoại lệ
                        throw;
                    }
                }
                // Chuyển hướng về hành động Index sau khi cập nhật thành công
                return RedirectToAction(nameof(Index));
            }

            // Nếu model không hợp lệ, chuẩn bị dữ liệu cho các dropdown trong view
            ViewData["IdDangTaiLieu"] = new SelectList(_context.DmDangTaiLieus, "IdDangTaiLieu", "DangTaiLieu", tbSachDaXuatBan.IdDangTaiLieu);
            ViewData["IdLoaiSachTapChi"] = new SelectList(_context.DmLoaiSachTapChis, "IdLoaiSachTapChi", "LoaiSachTapChi", tbSachDaXuatBan.IdLoaiSachTapChi);
            ViewData["IdNhiemVuKhcn"] = new SelectList(_context.TbNhiemVuKhcns, "IdNhiemVuKhcn", "IdNhiemVuKhcn", tbSachDaXuatBan.IdNhiemVuKhcn);

            // Trả về view với đối tượng tbSachDaXuatBan để hiển thị lại thông tin
            return View(tbSachDaXuatBan);
        }


        // GET: SachDaXuatBan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSachDaXuatBan = await _context.TbSachDaXuatBans
                .Include(t => t.IdDangTaiLieuNavigation)
                .Include(t => t.IdLoaiSachTapChiNavigation)
                .Include(t => t.IdNhiemVuKhcnNavigation)
                .FirstOrDefaultAsync(m => m.IdSachDaXuatBan == id);
            if (tbSachDaXuatBan == null)
            {
                return NotFound();
            }

            return View(tbSachDaXuatBan);
        }

        // POST: SachDaXuatBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbSachDaXuatBan = await _context.TbSachDaXuatBans.FindAsync(id);
            if (tbSachDaXuatBan != null)
            {
                _context.TbSachDaXuatBans.Remove(tbSachDaXuatBan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbSachDaXuatBanExists(int id)
        {
            return _context.TbSachDaXuatBans.Any(e => e.IdSachDaXuatBan == id);
        }
    }
}
