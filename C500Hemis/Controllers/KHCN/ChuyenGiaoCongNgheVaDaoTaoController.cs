using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C500Hemis.Models;
using Microsoft.CodeAnalysis.Options;

namespace C500Hemis.Controllers.KHCN
{
    public class ChuyenGiaoCongNgheVaDaoTaoController : Controller
    {
        private readonly HemisContext _context;

        public ChuyenGiaoCongNgheVaDaoTaoController(HemisContext context)
        {
            _context = context;
        }

        // GET: ChuyenGiaoCongNgheVaDaoTao
        //public async Task<IActionResult> Index()
        //{
        //    var hemisContext = _context.TbChuyenGiaoCongNgheVaDaoTaos.Include(t => t.IdHinhThucChuyenGiaoCongNgheNavigation).Include(t => t.IdLinhVucNghienCuuNavigation).Include(t => t.IdNganhKinhTeNavigation).Include(t => t.IdNhiemVuKhcnNavigation).Include(t => t.IdTrangThaiHopDongNavigation);
        //    return View(await hemisContext.ToListAsync());
        //}
        public IActionResult Index(string Id, string SapXep)
        {
            ViewBag.IdChuyenGiaoCongNgheVaDaoTao = Id;
            HemisContext db = new HemisContext();
            var kq = db.TbChuyenGiaoCongNgheVaDaoTaos.ToList(); // lấy danh sách 
            var danhSach = db.TbChuyenGiaoCongNgheVaDaoTaos
                .Include(item => item.IdHinhThucChuyenGiaoCongNgheNavigation)
                .Include(item => item.IdLinhVucNghienCuuNavigation)
                .Include(item => item.IdNganhKinhTeNavigation)
                .Include(item => item.IdNhiemVuKhcnNavigation)
                .Include(item => item.IdTrangThaiHopDongNavigation)
                .Where(item => string.IsNullOrEmpty(Id) || item.IdChuyenGiaoCongNgheVaDaoTao.ToString() == Id) //  tìm kiếm theo IdChuyeenr giao công nghệ
                .ToList();

            var sapXepDanhSach = danhSach; // sắp xếp

            if (SapXep == "SapXep")
            {
                sapXepDanhSach = danhSach.OrderBy(x => x.TongChiPhiThucHien).ToList();// sắp xếp theo tổng chi phí thực hiện
            }

            ViewBag.KqTimKiem = danhSach;
            ViewBag.KqSapXep = sapXepDanhSach;

            return View(sapXepDanhSach);
        }

        // GET: ChuyenGiaoCongNgheVaDaoTao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbChuyenGiaoCongNgheVaDaoTao = await _context.TbChuyenGiaoCongNgheVaDaoTaos
                .Include(t => t.IdHinhThucChuyenGiaoCongNgheNavigation)
                .Include(t => t.IdLinhVucNghienCuuNavigation)
                .Include(t => t.IdNganhKinhTeNavigation)
                .Include(t => t.IdNhiemVuKhcnNavigation)
                .Include(t => t.IdTrangThaiHopDongNavigation)
                .FirstOrDefaultAsync(m => m.IdChuyenGiaoCongNgheVaDaoTao == id);
            if (tbChuyenGiaoCongNgheVaDaoTao == null)
            {
                return NotFound();
            }

            return View(tbChuyenGiaoCongNgheVaDaoTao);
        }

        // GET: ChuyenGiaoCongNgheVaDaoTao/Create
        public IActionResult Create()
        {
           // _context.TbCanBos.Include(h => h.IdNguoiNavigation), "IdCanBo", "IdNguoiNavigation.name", tbThongTinHocTapNghienCuuSinh.IdNguoiHuongDanChinh
            ViewData["IdHinhThucChuyenGiaoCongNghe"] = new SelectList(_context.DmHinhThucChuyenGiaoCongNghes, "IdHinhThucChuyenGiaoCongNghe", "HinhThucChuyenGiaoCongNghe");
            ViewData["IdLinhVucNghienCuu"] = new SelectList(_context.DmLinhVucNghienCuus, "IdLinhVucNghienCuu", "LinhVucNghienCuu");
            ViewData["IdNganhKinhTe"] = new SelectList(_context.DmNganhKinhTes, "IdNganhKinhTe", "NganhKinhTe");
            ViewData["IdNhiemVuKhcn"] = new SelectList(_context.TbNhiemVuKhcns, "IdNhiemVuKhcn", "TenNhiemVu");
            ViewData["IdTrangThaiHopDong"] = new SelectList(_context.DmTrangThaiHopDongs, "IdTrangThaiHopDong", "TrangThaiHopDong");
          
            return View();
        }

        // POST: ChuyenGiaoCongNgheVaDaoTao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdChuyenGiaoCongNgheVaDaoTao,IdNhiemVuKhcn,MaSoHopDong,Ten,TongChiPhiThucHien,TongThoiGianThucHien,IdHinhThucChuyenGiaoCongNghe,PhuongThucChuyenGiaoCongNghe,ChuSoHuu,DonViChuTri,DonViPhoiHop,DonViNhanChuyenGiao,TomTat,TenDuAn,GiaTriHopDong,IdNganhKinhTe,IdTrangThaiHopDong,SoNguoiDuocDaoTaoChuyenGiaoCn,IdLinhVucNghienCuu")] TbChuyenGiaoCongNgheVaDaoTao tbChuyenGiaoCongNgheVaDaoTao)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(tbChuyenGiaoCongNgheVaDaoTao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Handle specific database update errors
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("PRIMARY KEY"))
                    {
                        ModelState.AddModelError("IdChuyenGiaoCongNgheVaDaoTao", "Mã chuyển giao công nghệ đã tồn tại.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi lưu dữ liệu. Vui lòng thử lại.");
                    }
                }
                catch (OverflowException)
                {
                    ModelState.AddModelError(string.Empty, "Giá trị đã nhập vượt quá giới hạn cho phép. Vui lòng nhập giá trị nhỏ hơn.");
                }
            }

            // Populate dropdowns for the view
            ViewData["IdHinhThucChuyenGiaoCongNghe"] = new SelectList(_context.DmHinhThucChuyenGiaoCongNghes, "IdHinhThucChuyenGiaoCongNghe", "HinhThucChuyenGiaoCongNghe", tbChuyenGiaoCongNgheVaDaoTao.IdHinhThucChuyenGiaoCongNghe);
            ViewData["IdLinhVucNghienCuu"] = new SelectList(_context.DmLinhVucNghienCuus, "IdLinhVucNghienCuu", "LinhVucNghienCuu", tbChuyenGiaoCongNgheVaDaoTao.IdLinhVucNghienCuu);
            ViewData["IdNganhKinhTe"] = new SelectList(_context.DmNganhKinhTes, "IdNganhKinhTe", "NganhKinhTe", tbChuyenGiaoCongNgheVaDaoTao.IdNganhKinhTe);
            ViewData["IdNhiemVuKhcn"] = new SelectList(_context.TbNhiemVuKhcns, "IdNhiemVuKhcn", "TenNhiemVu", tbChuyenGiaoCongNgheVaDaoTao.IdNhiemVuKhcn);
            ViewData["IdTrangThaiHopDong"] = new SelectList(_context.DmTrangThaiHopDongs, "IdTrangThaiHopDong", "TrangThaiHopDong", tbChuyenGiaoCongNgheVaDaoTao.IdTrangThaiHopDong);

            return View(tbChuyenGiaoCongNgheVaDaoTao);
        }

        //   <option value = "0" > @ViewBag.VuiLongChon </ option > 

        // GET: ChuyenGiaoCongNgheVaDaoTao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbChuyenGiaoCongNgheVaDaoTao = await _context.TbChuyenGiaoCongNgheVaDaoTaos.FindAsync(id);
            if (tbChuyenGiaoCongNgheVaDaoTao == null)
            {
                return NotFound();
            }
            ViewData["IdHinhThucChuyenGiaoCongNghe"] = new SelectList(_context.DmHinhThucChuyenGiaoCongNghes, "IdHinhThucChuyenGiaoCongNghe", "HinhThucChuyenGiaoCongNghe", tbChuyenGiaoCongNgheVaDaoTao.IdHinhThucChuyenGiaoCongNghe);
            ViewData["IdLinhVucNghienCuu"] = new SelectList(_context.DmLinhVucNghienCuus, "IdLinhVucNghienCuu", "LinhVucNghienCuu", tbChuyenGiaoCongNgheVaDaoTao.IdLinhVucNghienCuu);
            ViewData["IdNganhKinhTe"] = new SelectList(_context.DmNganhKinhTes, "IdNganhKinhTe", "NganhKinhTe", tbChuyenGiaoCongNgheVaDaoTao.IdNganhKinhTe);
            ViewData["IdNhiemVuKhcn"] = new SelectList(_context.TbNhiemVuKhcns, "IdNhiemVuKhcn", "TenNhiemVu", tbChuyenGiaoCongNgheVaDaoTao.IdNhiemVuKhcn);
            ViewData["IdTrangThaiHopDong"] = new SelectList(_context.DmTrangThaiHopDongs, "IdTrangThaiHopDong", "TrangThaiHopDong", tbChuyenGiaoCongNgheVaDaoTao.IdTrangThaiHopDong);
            return View(tbChuyenGiaoCongNgheVaDaoTao);
        }

        // POST: ChuyenGiaoCongNgheVaDaoTao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdChuyenGiaoCongNgheVaDaoTao,IdNhiemVuKhcn,MaSoHopDong,Ten,TongChiPhiThucHien,TongThoiGianThucHien,IdHinhThucChuyenGiaoCongNghe,PhuongThucChuyenGiaoCongNghe,ChuSoHuu,DonViChuTri,DonViPhoiHop,DonViNhanChuyenGiao,TomTat,TenDuAn,GiaTriHopDong,IdNganhKinhTe,IdTrangThaiHopDong,SoNguoiDuocDaoTaoChuyenGiaoCn,IdLinhVucNghienCuu")] TbChuyenGiaoCongNgheVaDaoTao tbChuyenGiaoCongNgheVaDaoTao)
        {
            if (id != tbChuyenGiaoCongNgheVaDaoTao.IdChuyenGiaoCongNgheVaDaoTao)
            {
                return NotFound();
            }
            // kiểm tra xem dữ liệu có tồn tại không
            if (ModelState.IsValid)
            {
                try
                {
                    // update cái dữ liệu mới
                    _context.Update(tbChuyenGiaoCongNgheVaDaoTao);
                    // lưu thay đổi
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // xem có tồn tại cái id này không, nếu k thi 404
                    if (!TbChuyenGiaoCongNgheVaDaoTaoExists(tbChuyenGiaoCongNgheVaDaoTao.IdChuyenGiaoCongNgheVaDaoTao))
                    {
                        return NotFound();
                    }
                    else
                    {// ném looix
                        throw;
                    }
                }
                //quay lại trang index
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHinhThucChuyenGiaoCongNghe"] = new SelectList(_context.DmHinhThucChuyenGiaoCongNghes, "IdHinhThucChuyenGiaoCongNghe", "HinhThucChuyenGiaoCongNghe", tbChuyenGiaoCongNgheVaDaoTao.IdHinhThucChuyenGiaoCongNghe);
            ViewData["IdLinhVucNghienCuu"] = new SelectList(_context.DmLinhVucNghienCuus, "IdLinhVucNghienCuu", "LinhVucNghienCuu", tbChuyenGiaoCongNgheVaDaoTao.IdLinhVucNghienCuu);
            ViewData["IdNganhKinhTe"] = new SelectList(_context.DmNganhKinhTes, "IdNganhKinhTe", "NganhKinhTe", tbChuyenGiaoCongNgheVaDaoTao.IdNganhKinhTe);
            ViewData["IdNhiemVuKhcn"] = new SelectList(_context.TbNhiemVuKhcns, "IdNhiemVuKhcn", "TenNhiemVu", tbChuyenGiaoCongNgheVaDaoTao.IdNhiemVuKhcn);
            ViewData["IdTrangThaiHopDong"] = new SelectList(_context.DmTrangThaiHopDongs, "IdTrangThaiHopDong", "TrangThaiHopDong", tbChuyenGiaoCongNgheVaDaoTao.IdTrangThaiHopDong);
            return View(tbChuyenGiaoCongNgheVaDaoTao);
        }

        // GET: ChuyenGiaoCongNgheVaDaoTao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)// ktra xem id có null khôg
            {
                return NotFound();// nếu có thì 400
            }

            var tbChuyenGiaoCongNgheVaDaoTao = await _context.TbChuyenGiaoCongNgheVaDaoTaos
                .Include(t => t.IdHinhThucChuyenGiaoCongNgheNavigation)
                .Include(t => t.IdLinhVucNghienCuuNavigation)
                .Include(t => t.IdNganhKinhTeNavigation)
                .Include(t => t.IdNhiemVuKhcnNavigation)
                .Include(t => t.IdTrangThaiHopDongNavigation)
                .FirstOrDefaultAsync(m => m.IdChuyenGiaoCongNgheVaDaoTao == id); // tìm kiếm giá trị ban đầu
            if (tbChuyenGiaoCongNgheVaDaoTao == null)
            {
                return NotFound();
            }

            return View(tbChuyenGiaoCongNgheVaDaoTao);
        }

        // POST: ChuyenGiaoCongNgheVaDaoTao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbChuyenGiaoCongNgheVaDaoTao = await _context.TbChuyenGiaoCongNgheVaDaoTaos.FindAsync(id);
            if (tbChuyenGiaoCongNgheVaDaoTao != null)
            {
                _context.TbChuyenGiaoCongNgheVaDaoTaos.Remove(tbChuyenGiaoCongNgheVaDaoTao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSelected([FromBody] List<int> selectedIds)
        {
            // kiểm tra những id được chọn
            if (selectedIds == null || selectedIds.Count == 0)
                
            {
                // nếu không thì báo như của message
                return BadRequest(new { message = "Không có mục nào được chọn." });
            }
            // lấy danh sách mà những id dc chọn
            var itemsToDelete = _context.TbChuyenGiaoCongNgheVaDaoTaos
                .Where(x => selectedIds.Contains(x.IdChuyenGiaoCongNgheVaDaoTao))
                .ToList();

            if (itemsToDelete.Any())
            {
                // remove nó
                _context.TbChuyenGiaoCongNgheVaDaoTaos.RemoveRange(itemsToDelete);
                _context.SaveChanges();
               // alert báo thành công
                return Ok(new { message = "Xóa thành công." });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy các mục đã chọn." });
            }
        }

        private bool TbChuyenGiaoCongNgheVaDaoTaoExists(int id)
        {
            return _context.TbChuyenGiaoCongNgheVaDaoTaos.Any(e => e.IdChuyenGiaoCongNgheVaDaoTao == id);
        }
    }
}
