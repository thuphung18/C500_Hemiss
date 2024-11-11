using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C500Hemis.Models;

namespace C500Hemis.Controllers.NH
{
    public class KyLuatNguoiHocController : Controller
    {
        private readonly HemisContext _context;

        public KyLuatNguoiHocController(HemisContext context)
        {
            _context = context;
        }
        // Toàn bộ action mặc định không xuất lổi chỉ trả về BadRequest
        // GET: KyLuatNguoiHoc
        public async Task<IActionResult> Index()
        {
            try
            {
                var hemisContext = _context.TbKyLuatNguoiHocs.Include(t => t.IdCapQuyetDinhNavigation).Include(t => t.IdHocVienNavigation).ThenInclude(human => human.IdNguoiNavigation).Include(t => t.IdLoaiKyLuatNavigation);
                return View(await hemisContext.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: KyLuatNguoiHoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbKyLuatNguoiHoc = await _context.TbKyLuatNguoiHocs
                    .Include(t => t.IdCapQuyetDinhNavigation)
                    .Include(t => t.IdHocVienNavigation).ThenInclude(human => human.IdNguoiNavigation)
                    .Include(t => t.IdLoaiKyLuatNavigation)
                    .FirstOrDefaultAsync(m => m.IdKyLuatNguoiHoc == id);
                if (tbKyLuatNguoiHoc == null)
                {
                    return NotFound();
                }

                return View(tbKyLuatNguoiHoc);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Hàm khởi tạo thông tin kỷ luật người học
        /// </summary>
        /// <returns></returns>
        // GET: KyLuatNguoiHoc/Create
        public IActionResult Create()
        {
            try
            {
                //Lấy danh sách cấp quyết định, hiện thị cụ thể cấp quyết định
                ViewData["IdCapQuyetDinh"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "CapKhenThuong");

                //Lấy danh sách người học truyền cho selectbox người học bên view
                ViewData["IdHocVien"] = new SelectList(_context.TbHocViens.Include(e => e.IdNguoiNavigation), "IdHocVien", "IdNguoiNavigation.name");

                //Lấy danh sách các loại kỷ luật, hiện thị cụ thể tên loại kỷ luật
                ViewData["IdLoaiKyLuat"] = new SelectList(_context.DmLoaiKyLuats, "IdLoaiKyLuat", "LoaiKyLuat");
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            //lấy thông tin từ các bảng khác nếu có lỗi trả về badrequest
        }
        // POST: KyLuatNguoiHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKyLuatNguoiHoc,IdHocVien,IdLoaiKyLuat,LyDo,IdCapQuyetDinh,SoQuyetDinh,NgayQuyetDinh,NamBiKyLuat")] TbKyLuatNguoiHoc tbKyLuatNguoiHoc)
        {
            try
            {
                //Nếu đã tồn tại thì thêm Error cho IdKyLuatNguoiHoc
                if (await existId(tbKyLuatNguoiHoc.IdKyLuatNguoiHoc)) ModelState.AddModelError("IdKyLuatNguoiHoc", "Đã tồn tại Id này!");
                if (ModelState.IsValid)
                {
                    //Thêm đối tượng vào context
                    _context.Add(tbKyLuatNguoiHoc);

                    // Lưu vào cơ sở dữ liệu
                    await _context.SaveChangesAsync();

                    // Nếu thành công sẽ trở về trang index
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdCapQuyetDinh"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "CapKhenThuong", tbKyLuatNguoiHoc.IdCapQuyetDinh);
                ViewData["IdHocVien"] = new SelectList(_context.TbHocViens.Include(e => e.IdNguoiNavigation), "IdHocVien", "IdNguoiNavigation.name", tbKyLuatNguoiHoc.IdHocVien);
                ViewData["IdLoaiKyLuat"] = new SelectList(_context.DmLoaiKyLuats, "IdLoaiKyLuat", "LoaiKyLuat", tbKyLuatNguoiHoc.IdLoaiKyLuat);
                return View(tbKyLuatNguoiHoc);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: KyLuatNguoiHoc/Edit/5
        /// <summary>
        /// Khởi tạo sưa thông tin kỷ luật
        /// </summary>
        /// <param name="id"> là id định danh của Kỷ luật Người Học trong cơ sở dữ liệu </param>
        /// <returns>View khởi tạo kỷ luật người học</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbKyLuatNguoiHoc = await _context.TbKyLuatNguoiHocs.FindAsync(id);
                if (tbKyLuatNguoiHoc == null)
                {
                    return NotFound();
                }
                ViewData["IdCapQuyetDinh"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "CapKhenThuong", tbKyLuatNguoiHoc.IdCapQuyetDinh);
                ViewData["IdHocVien"] = new SelectList(_context.TbHocViens.Include(e => e.IdNguoiNavigation), "IdHocVien", "IdNguoiNavigation.name", tbKyLuatNguoiHoc.IdHocVien);
                ViewData["IdLoaiKyLuat"] = new SelectList(_context.DmLoaiKyLuats, "IdLoaiKyLuat", "LoaiKyLuat", tbKyLuatNguoiHoc.IdLoaiKyLuat);
                return View(tbKyLuatNguoiHoc);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST: KyLuatNguoiHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKyLuatNguoiHoc,IdHocVien,IdLoaiKyLuat,LyDo,IdCapQuyetDinh,SoQuyetDinh,NgayQuyetDinh,NamBiKyLuat")] TbKyLuatNguoiHoc tbKyLuatNguoiHoc)
        {
            try
            {
                if (id != tbKyLuatNguoiHoc.IdKyLuatNguoiHoc)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(tbKyLuatNguoiHoc);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!(await existId(tbKyLuatNguoiHoc.IdKyLuatNguoiHoc)))
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
                ViewData["IdCapQuyetDinh"] = new SelectList(_context.DmCapKhenThuongs, "IdCapKhenThuong", "CapKhenThuong", tbKyLuatNguoiHoc.IdCapQuyetDinh);
                ViewData["IdHocVien"] = new SelectList(_context.TbHocViens.Include(e => e.IdNguoiNavigation), "IdHocVien", "IdNguoiNavigation.name", tbKyLuatNguoiHoc.IdHocVien);
                ViewData["IdLoaiKyLuat"] = new SelectList(_context.DmLoaiKyLuats, "IdLoaiKyLuat", "LoaiKyLuat", tbKyLuatNguoiHoc.IdLoaiKyLuat);
                return View(tbKyLuatNguoiHoc);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: KyLuatNguoiHoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbKyLuatNguoiHoc = await _context.TbKyLuatNguoiHocs
                    .Include(t => t.IdCapQuyetDinhNavigation)
                    .Include(t => t.IdHocVienNavigation)
                    .ThenInclude(human => human.IdNguoiNavigation)
                    .Include(t => t.IdLoaiKyLuatNavigation)
                    .FirstOrDefaultAsync(m => m.IdKyLuatNguoiHoc == id);
                if (tbKyLuatNguoiHoc == null)
                {
                    return NotFound();
                }

                return View(tbKyLuatNguoiHoc);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST: KyLuatNguoiHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var tbKyLuatNguoiHoc = await _context.TbKyLuatNguoiHocs.FindAsync(id);
                if (tbKyLuatNguoiHoc != null)
                {
                    _context.TbKyLuatNguoiHocs.Remove(tbKyLuatNguoiHoc);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        private async Task<bool> existId(int id)
        {
            //Kiểm tra đã tồn tại trong TbKyLuatNguoiHoc chua
            TbKyLuatNguoiHoc? cr = (await _context.TbKyLuatNguoiHocs.SingleOrDefaultAsync(e => e.IdKyLuatNguoiHoc == id));
            if (cr == null) return false;
            return true;
        }
    }
}
