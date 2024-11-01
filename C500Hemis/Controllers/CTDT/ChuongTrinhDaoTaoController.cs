// Nguyễn Đăng Phúc 023-B8D55 (Bài kiểm tra giữa phần)
// Các nội dung đã làm:
//      + Sửa các khóa ngoài để hiển thị nội dung trong phần Views
//      + Thêm các comment chi tiết phục vụ bảo trì và fix bug.
//      + Bắt lỗi trùng Id, thông báo và gọi người dùng nhập lại.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C500Hemis.Models;

namespace C500Hemis.Controllers.CTDT
{
    public class ChuongTrinhDaoTaoController : Controller
    {
        private readonly HemisContext _context;

        // Lấy từ HemisContext 
        public ChuongTrinhDaoTaoController(HemisContext context)
        {
            _context = context;
        }

        // GET: ChuongTrinhDaoTao
        // Lấy danh sách CTĐT từ database, trả về view Index.
        public async Task<IActionResult> Index()
        {
            try
            {
                var hemisContext = _context.TbChuongTrinhDaoTaos
                // Lấy data từ các table khác có liên quan (khóa ngoài) để hiển thị trên Index
                .Include(t => t.IdDonViCapBangNavigation)
                .Include(t => t.IdHocCheDaoTaoNavigation)
                .Include(t => t.IdLoaiChuongTrinhDaoTaoNavigation)
                .Include(t => t.IdLoaiChuongTrinhLienKetDaoTaoNavigation)
                .Include(t => t.IdNganhDaoTaoNavigation)
                .Include(t => t.IdQuocGiaCuaTruSoChinhNavigation)
                .Include(t => t.IdTrangThaiCuaChuongTrinhNavigation)
                .Include(t => t.IdTrinhDoDaoTaoNavigation);
                return View(await hemisContext.ToListAsync());

                // Bắt lỗi các trường hợp ngoại lệ
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // Lấy chi tiết 1 bản ghi dựa theo ID tương ứng đã truyền vào (IdChuongTrinhDaoTao)
        // Hiển thị bản ghi đó ở view Details
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                // Tìm các dữ liệu theo Id tương ứng đã truyền vào view Details
                var tbChuongTrinhDaoTao = await _context.TbChuongTrinhDaoTaos
                    .Include(t => t.IdDonViCapBangNavigation)
                    .Include(t => t.IdHocCheDaoTaoNavigation)
                    .Include(t => t.IdLoaiChuongTrinhDaoTaoNavigation)
                    .Include(t => t.IdLoaiChuongTrinhLienKetDaoTaoNavigation)
                    .Include(t => t.IdNganhDaoTaoNavigation)
                    .Include(t => t.IdQuocGiaCuaTruSoChinhNavigation)
                    .Include(t => t.IdTrangThaiCuaChuongTrinhNavigation)
                    .Include(t => t.IdTrinhDoDaoTaoNavigation)
                    .FirstOrDefaultAsync(m => m.IdChuongTrinhDaoTao == id);

                // Nếu không tìm thấy Id tương ứng, chương trình sẽ báo lỗi NotFound
                if (tbChuongTrinhDaoTao == null)
                {
                    return NotFound();
                }
                // Nếu đã tìm thấy Id tương ứng, chương trình sẽ dẫn đến view Details
                // Hiển thị thông thi chi tiết CTĐT thành công
                return View(tbChuongTrinhDaoTao);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // GET: ChuongTrinhDaoTao/Create
        // Hiển thị view Create để tạo một bản ghi CTĐT mới
        // Truyền data từ các table khác hiển thị tại view Create (khóa ngoài)
        public IActionResult Create()
        {
            try
            {
                ViewData["IdDonViCapBang"] = new SelectList(_context.DmDonViCapBangs, "IdDonViCapBang", "DonViCapBang");
                ViewData["IdHocCheDaoTao"] = new SelectList(_context.DmHocCheDaoTaos, "IdHocCheDaoTao", "HocCheDaoTao");
                ViewData["IdLoaiChuongTrinhDaoTao"] = new SelectList(_context.DmLoaiChuongTrinhDaoTaos, "IdLoaiChuongTrinhDaoTao", "LoaiChuongTrinhDaoTao");
                ViewData["IdLoaiChuongTrinhLienKetDaoTao"] = new SelectList(_context.DmLoaiChuongTrinhLienKetDaoTaos, "IdLoaiChuongTrinhLienKetDaoTao", "LoaiChuongTrinhLienKetDaoTao");
                ViewData["IdNganhDaoTao"] = new SelectList(_context.DmNganhDaoTaos, "IdNganhDaoTao", "NganhDaoTao");
                ViewData["IdQuocGiaCuaTruSoChinh"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "TenNuoc");
                ViewData["IdTrangThaiCuaChuongTrinh"] = new SelectList(_context.DmTrangThaiChuongTrinhDaoTaos, "IdTrangThaiChuongTrinhDaoTao", "TrangThaiChuongTrinhDaoTao");
                ViewData["IdTrinhDoDaoTao"] = new SelectList(_context.DmTrinhDoDaoTaos, "IdTrinhDoDaoTao", "TrinhDoDaoTao");
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // POST: ChuongTrinhDaoTao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // Thêm một CTĐT mới vào Database nếu IdChuongTrinhDaoTao truyền vào không trùng với Id đã có trong Database
        // Trong trường hợp nhập trùng IdChuongTrinhDaoTao sẽ bắt lỗi
        // Bắt lỗi ngoại lệ sao cho người nhập BẮT BUỘC phải nhập khác IdChuongTrinhDaoTao đã có
        [HttpPost]
        [ValidateAntiForgeryToken] // Một phương thức bảo mật thông qua Token được tạo tự động cho các Form khác nhau
        public async Task<IActionResult> Create([Bind("IdChuongTrinhDaoTao,MaChuongTrinhDaoTao,IdNganhDaoTao,TenChuongTrinh,TenChuongTrinhBangTiengAnh,NamBatDauTuyenSinh,TenCoSoDaoTaoNuocNgoai,IdLoaiChuongTrinhDaoTao,IdLoaiChuongTrinhLienKetDaoTao,DiaDiemDaoTao,IdHocCheDaoTao,IdQuocGiaCuaTruSoChinh,NgayBanHanhChuanDauRa,IdTrinhDoDaoTao,ThoiGianDaoTaoChuan,ChuanDauRa,IdDonViCapBang,LoaiChungChiDuocChapThuan,DonViThucHienChuongTrinh,IdTrangThaiCuaChuongTrinh,ChuanDauRaVeNgoaiNgu,ChuanDauRaVeTinHoc,HocPhiTaiVietNam,HocPhiTaiNuocNgoai")] TbChuongTrinhDaoTao tbChuongTrinhDaoTao)
        {
            try
            {
                // Nếu trùng IDChuongTrinhDaoTao sẽ báo lỗi
                if (TbChuongTrinhDaoTaoExists(tbChuongTrinhDaoTao.IdChuongTrinhDaoTao)) ModelState.AddModelError("IdChuongTrinhDaoTao", "ID này đã tồn tại!");



                if (ModelState.IsValid)
                {
                    _context.Add(tbChuongTrinhDaoTao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdDonViCapBang"] = new SelectList(_context.DmDonViCapBangs, "IdDonViCapBang", "IdDonViCapBang", tbChuongTrinhDaoTao.IdDonViCapBang);
                ViewData["IdHocCheDaoTao"] = new SelectList(_context.DmHocCheDaoTaos, "IdHocCheDaoTao", "IdHocCheDaoTao", tbChuongTrinhDaoTao.IdHocCheDaoTao);
                ViewData["IdLoaiChuongTrinhDaoTao"] = new SelectList(_context.DmLoaiChuongTrinhDaoTaos, "IdLoaiChuongTrinhDaoTao", "IdLoaiChuongTrinhDaoTao", tbChuongTrinhDaoTao.IdLoaiChuongTrinhDaoTao);
                ViewData["IdLoaiChuongTrinhLienKetDaoTao"] = new SelectList(_context.DmLoaiChuongTrinhLienKetDaoTaos, "IdLoaiChuongTrinhLienKetDaoTao", "IdLoaiChuongTrinhLienKetDaoTao", tbChuongTrinhDaoTao.IdLoaiChuongTrinhLienKetDaoTao);
                ViewData["IdNganhDaoTao"] = new SelectList(_context.DmNganhDaoTaos, "IdNganhDaoTao", "IdNganhDaoTao", tbChuongTrinhDaoTao.IdNganhDaoTao);
                ViewData["IdQuocGiaCuaTruSoChinh"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "IdQuocTich", tbChuongTrinhDaoTao.IdQuocGiaCuaTruSoChinh);
                ViewData["IdTrangThaiCuaChuongTrinh"] = new SelectList(_context.DmTrangThaiChuongTrinhDaoTaos, "IdTrangThaiChuongTrinhDaoTao", "IdTrangThaiChuongTrinhDaoTao", tbChuongTrinhDaoTao.IdTrangThaiCuaChuongTrinh);
                ViewData["IdTrinhDoDaoTao"] = new SelectList(_context.DmTrinhDoDaoTaos, "IdTrinhDoDaoTao", "IdTrinhDoDaoTao", tbChuongTrinhDaoTao.IdTrinhDoDaoTao);
                return View(tbChuongTrinhDaoTao);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // GET: ChuongTrinhDaoTao/Edit
        // Lấy data từ Database với Id đã có, sau đó hiển thị ở view Edit
        // Nếu không tìm thấy Id tương ứng sẽ báo lỗi NotFound
        // Phương thức này gần giống Create, nhưng nó nhập dữ liệu vào Id đã có trong database
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbChuongTrinhDaoTao = await _context.TbChuongTrinhDaoTaos.FindAsync(id);
                if (tbChuongTrinhDaoTao == null)
                {
                    return NotFound();
                }
                ViewData["IdDonViCapBang"] = new SelectList(_context.DmDonViCapBangs, "IdDonViCapBang", "DonViCapBang", tbChuongTrinhDaoTao.IdDonViCapBang);
                ViewData["IdHocCheDaoTao"] = new SelectList(_context.DmHocCheDaoTaos, "IdHocCheDaoTao", "HocCheDaoTao", tbChuongTrinhDaoTao.IdHocCheDaoTao);
                ViewData["IdLoaiChuongTrinhDaoTao"] = new SelectList(_context.DmLoaiChuongTrinhDaoTaos, "IdLoaiChuongTrinhDaoTao", "LoaiChuongTrinhDaoTao", tbChuongTrinhDaoTao.IdLoaiChuongTrinhDaoTao);
                ViewData["IdLoaiChuongTrinhLienKetDaoTao"] = new SelectList(_context.DmLoaiChuongTrinhLienKetDaoTaos, "IdLoaiChuongTrinhLienKetDaoTao", "LoaiChuongTrinhLienKetDaoTao", tbChuongTrinhDaoTao.IdLoaiChuongTrinhLienKetDaoTao);
                ViewData["IdNganhDaoTao"] = new SelectList(_context.DmNganhDaoTaos, "IdNganhDaoTao", "NganhDaoTao", tbChuongTrinhDaoTao.IdNganhDaoTao);
                ViewData["IdQuocGiaCuaTruSoChinh"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "TenNuoc", tbChuongTrinhDaoTao.IdQuocGiaCuaTruSoChinh);
                ViewData["IdTrangThaiCuaChuongTrinh"] = new SelectList(_context.DmTrangThaiChuongTrinhDaoTaos, "IdTrangThaiChuongTrinhDaoTao", "TrangThaiChuongTrinhDaoTao", tbChuongTrinhDaoTao.IdTrangThaiCuaChuongTrinh);
                ViewData["IdTrinhDoDaoTao"] = new SelectList(_context.DmTrinhDoDaoTaos, "IdTrinhDoDaoTao", "TrinhDoDaoTao", tbChuongTrinhDaoTao.IdTrinhDoDaoTao);
                return View(tbChuongTrinhDaoTao);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // POST: ChuongTrinhDaoTao/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // Lưu data mới (ghi đè) vào các trường Data đã có thuộc IdChuongTrinhDaoTao cần chỉnh sửa
        // Nó chỉ cập nhật khi ModelState hợp lệ
        // Nếu không hợp lệ sẽ báo lỗi, vì vậy cần có bắt lỗi.

        [HttpPost]
        [ValidateAntiForgeryToken] // Một phương thức bảo mật thông qua Token được tạo tự động cho các Form khác nhau
        public async Task<IActionResult> Edit(int id, [Bind("IdChuongTrinhDaoTao,MaChuongTrinhDaoTao,IdNganhDaoTao,TenChuongTrinh,TenChuongTrinhBangTiengAnh,NamBatDauTuyenSinh,TenCoSoDaoTaoNuocNgoai,IdLoaiChuongTrinhDaoTao,IdLoaiChuongTrinhLienKetDaoTao,DiaDiemDaoTao,IdHocCheDaoTao,IdQuocGiaCuaTruSoChinh,NgayBanHanhChuanDauRa,IdTrinhDoDaoTao,ThoiGianDaoTaoChuan,ChuanDauRa,IdDonViCapBang,LoaiChungChiDuocChapThuan,DonViThucHienChuongTrinh,IdTrangThaiCuaChuongTrinh,ChuanDauRaVeNgoaiNgu,ChuanDauRaVeTinHoc,HocPhiTaiVietNam,HocPhiTaiNuocNgoai")] TbChuongTrinhDaoTao tbChuongTrinhDaoTao)
        {
            try
            {
                if (id != tbChuongTrinhDaoTao.IdChuongTrinhDaoTao)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(tbChuongTrinhDaoTao);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TbChuongTrinhDaoTaoExists(tbChuongTrinhDaoTao.IdChuongTrinhDaoTao))
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
                ViewData["IdDonViCapBang"] = new SelectList(_context.DmDonViCapBangs, "IdDonViCapBang", "IdDonViCapBang", tbChuongTrinhDaoTao.IdDonViCapBang);
                ViewData["IdHocCheDaoTao"] = new SelectList(_context.DmHocCheDaoTaos, "IdHocCheDaoTao", "IdHocCheDaoTao", tbChuongTrinhDaoTao.IdHocCheDaoTao);
                ViewData["IdLoaiChuongTrinhDaoTao"] = new SelectList(_context.DmLoaiChuongTrinhDaoTaos, "IdLoaiChuongTrinhDaoTao", "IdLoaiChuongTrinhDaoTao", tbChuongTrinhDaoTao.IdLoaiChuongTrinhDaoTao);
                ViewData["IdLoaiChuongTrinhLienKetDaoTao"] = new SelectList(_context.DmLoaiChuongTrinhLienKetDaoTaos, "IdLoaiChuongTrinhLienKetDaoTao", "IdLoaiChuongTrinhLienKetDaoTao", tbChuongTrinhDaoTao.IdLoaiChuongTrinhLienKetDaoTao);
                ViewData["IdNganhDaoTao"] = new SelectList(_context.DmNganhDaoTaos, "IdNganhDaoTao", "IdNganhDaoTao", tbChuongTrinhDaoTao.IdNganhDaoTao);
                ViewData["IdQuocGiaCuaTruSoChinh"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "IdQuocTich", tbChuongTrinhDaoTao.IdQuocGiaCuaTruSoChinh);
                ViewData["IdTrangThaiCuaChuongTrinh"] = new SelectList(_context.DmTrangThaiChuongTrinhDaoTaos, "IdTrangThaiChuongTrinhDaoTao", "IdTrangThaiChuongTrinhDaoTao", tbChuongTrinhDaoTao.IdTrangThaiCuaChuongTrinh);
                ViewData["IdTrinhDoDaoTao"] = new SelectList(_context.DmTrinhDoDaoTaos, "IdTrinhDoDaoTao", "IdTrinhDoDaoTao", tbChuongTrinhDaoTao.IdTrinhDoDaoTao);
                return View(tbChuongTrinhDaoTao);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // GET: ChuongTrinhDaoTao/Delete
        // Xóa một CTĐT khỏi Database
        // Lấy data CTĐT từ Database, hiển thị Data tại view Delete
        // Hàm này để hiển thị thông tin cho người dùng trước khi xóa
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbChuongTrinhDaoTao = await _context.TbChuongTrinhDaoTaos
                    .Include(t => t.IdDonViCapBangNavigation)
                    .Include(t => t.IdHocCheDaoTaoNavigation)
                    .Include(t => t.IdLoaiChuongTrinhDaoTaoNavigation)
                    .Include(t => t.IdLoaiChuongTrinhLienKetDaoTaoNavigation)
                    .Include(t => t.IdNganhDaoTaoNavigation)
                    .Include(t => t.IdQuocGiaCuaTruSoChinhNavigation)
                    .Include(t => t.IdTrangThaiCuaChuongTrinhNavigation)
                    .Include(t => t.IdTrinhDoDaoTaoNavigation)
                    .FirstOrDefaultAsync(m => m.IdChuongTrinhDaoTao == id);
                if (tbChuongTrinhDaoTao == null)
                {
                    return NotFound();
                }

                return View(tbChuongTrinhDaoTao);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // POST: ChuongTrinhDaoTao/Delete
        // Xóa CTĐT khỏi Database sau khi nhấn xác nhận 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) // Lệnh xác nhận xóa hẳn một CTĐT
        {
            try
            {
                var tbChuongTrinhDaoTao = await _context.TbChuongTrinhDaoTaos.FindAsync(id);
                if (tbChuongTrinhDaoTao != null)
                {
                    _context.TbChuongTrinhDaoTaos.Remove(tbChuongTrinhDaoTao);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }

        private bool TbChuongTrinhDaoTaoExists(int id)
        {
            return _context.TbChuongTrinhDaoTaos.Any(e => e.IdChuongTrinhDaoTao == id);
        }
    }
}
