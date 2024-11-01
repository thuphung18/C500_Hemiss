using C500Hemis.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace C500Hemis.Controllers.TS
{
    public class TbDuLieuTrungTuyensController : Controller
    {
        private readonly HemisContext _context;

        public TbDuLieuTrungTuyensController(HemisContext context)
        {
            _context = context;
        }

        // GET: TbDuLieuTrungTuyens
        public async Task<IActionResult> Index()
        {
            var hemisContext = _context.TbDuLieuTrungTuyens.Include(t => t.IdDoiTuongDauVaoNavigation).Include(t => t.IdDoiTuongUuTienNavigation).Include(t => t.IdHinhThucTuyenSinhNavigation).Include(t => t.IdKhuVucNavigation);
            return View(await hemisContext.ToListAsync());
        }

        // GET: TbDuLieuTrungTuyens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {

                if (id == null)
                {
                    return NotFound();
                }

                var tbDuLieuTrungTuyen = await _context.TbDuLieuTrungTuyens
                    .Include(t => t.IdDoiTuongDauVaoNavigation)
                    .Include(t => t.IdDoiTuongUuTienNavigation)
                    .Include(t => t.IdHinhThucTuyenSinhNavigation)
                    .Include(t => t.IdKhuVucNavigation)
                    .FirstOrDefaultAsync(m => m.IdDuLieuTrungTuyen == id);
                if (tbDuLieuTrungTuyen == null)
                {
                    return NotFound();
                }

                return View(tbDuLieuTrungTuyen);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: TbDuLieuTrungTuyens/Create
        public IActionResult Create()
        {
            ViewData["IdDoiTuongDauVao"] = new SelectList(_context.DmDoiTuongDauVaos, "IdDoiTuongDauVao", "DoiTuongDauVao");
            ViewData["IdDoiTuongUuTien"] = new SelectList(_context.DmDoiTuongUuTiens, "IdDoiTuongUuTien", "DoiTuongUuTien");
            ViewData["IdHinhThucTuyenSinh"] = new SelectList(_context.DmHinhThucTuyenSinhs, "IdHinhThucTuyenSinh", "HinhThucTuyenSinh");
            ViewData["IdKhuVuc"] = new SelectList(_context.DmKhuVucs, "IdKhuVuc", "KhuVuc");
            return View();
        }

        // POST: TbDuLieuTrungTuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDuLieuTrungTuyen,Cccd,HoVaTen,MaTuyenSinh,KhoaDaoTaoTrungTuyen,IdDoiTuongDauVao,IdDoiTuongUuTien,IdHinhThucTuyenSinh,IdKhuVuc,TruongThpt,ToHopMonTrungTuyen,DiemMon1,DiemMon2,DiemMon3,DiemUuTien,TongDiemXetTuyen,SoQuyetDinhTrungTuyen,NgayBanHanhQuyetDinhTrungTuyen,ChuongTrinhDaoTaoDaTotNghiepTrinhDoDaiHoc,NganhDaTotNghiepTrinhDoDaiHoc,ChuongTrinhDaoTaoDaTotNghiepTrinhDoThacSi,NganhDaTotNghiepTrinhDoThacSi")] TbDuLieuTrungTuyen tbDuLieuTrungTuyen)
        {

            try
            {
                //Ktra các trường cần thiết
                if (tbDuLieuTrungTuyen.IdDuLieuTrungTuyen == null)
                {
                    ModelState.AddModelError("IdDuLieuTrungTuyen", "Id dữ liệu trúng tuyển không được để trống");
                }
                if (string.IsNullOrWhiteSpace(tbDuLieuTrungTuyen.HoVaTen))
                {
                    ModelState.AddModelError("HoVaTen", "Họ Tên không được để trống");

                }
                if (string.IsNullOrWhiteSpace(tbDuLieuTrungTuyen.MaTuyenSinh))
                {
                    ModelState.AddModelError("MaTuyenSinh", "Mã Tuyển Sinh không được để trống");

                }
                if (string.IsNullOrWhiteSpace(tbDuLieuTrungTuyen.KhoaDaoTaoTrungTuyen))
                {
                    ModelState.AddModelError("KhoaDaoTaoTrungTuyen", "Khoa Đào Tạo Trúng Tuyển không được để trống");

                }
                if (string.IsNullOrWhiteSpace(tbDuLieuTrungTuyen.TruongThpt))
                {
                    ModelState.AddModelError("TruongThpt", "Tên Trường THPT không được để trống");

                }
                if (string.IsNullOrWhiteSpace(tbDuLieuTrungTuyen.ToHopMonTrungTuyen))
                {
                    ModelState.AddModelError("ToHopMonTrungTuyen", "Tổ Hợp Môn Trúng Tuyển không được để trống");

                }
                if (tbDuLieuTrungTuyen.DiemMon1 == null)
                {
                    ModelState.AddModelError("DiemMon1", "Điểm Môn 1 không được để trống");

                }
                if (tbDuLieuTrungTuyen.DiemMon2 == null)
                {
                    ModelState.AddModelError("DiemMon2", "Điểm Môn 2 không được để trống");

                }
                if (tbDuLieuTrungTuyen.DiemMon3 == null)
                {
                    ModelState.AddModelError("DiemMon3", "Điểm Môn3 không được để trống");

                }
                if (tbDuLieuTrungTuyen.DiemUuTien == null)
                {
                    ModelState.AddModelError("DiemUuTien", "Điểm Ưu Tiên không được để trống");

                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Tính tổng điểm xét tuyển
                        tbDuLieuTrungTuyen.TongDiemXetTuyen = tbDuLieuTrungTuyen.DiemMon1 + tbDuLieuTrungTuyen.DiemMon2 + tbDuLieuTrungTuyen.DiemMon3 + tbDuLieuTrungTuyen.DiemUuTien;
                        //nếu ok thì lưu vào csdl
                        _context.Add(tbDuLieuTrungTuyen);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Đã xảy ra lỗi khi tạo mới: " + ex.Message);
                    }

                }
                else
                {
                    var error1 = ModelState.Values.SelectMany(h => h.Errors);
                    foreach (var error in error1)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }

                ViewData["IdDoiTuongDauVao"] = new SelectList(_context.DmDoiTuongDauVaos, "IdDoiTuongDauVao", "DoiTuongDauVao", tbDuLieuTrungTuyen.IdDoiTuongDauVao);
                ViewData["IdDoiTuongUuTien"] = new SelectList(_context.DmDoiTuongUuTiens, "IdDoiTuongUuTien", "DoiTuongUuTien", tbDuLieuTrungTuyen.IdDoiTuongUuTien);
                ViewData["IdHinhThucTuyenSinh"] = new SelectList(_context.DmHinhThucTuyenSinhs, "IdHinhThucTuyenSinh", "HinhThucTuyenSinh", tbDuLieuTrungTuyen.IdHinhThucTuyenSinh);
                ViewData["IdKhuVuc"] = new SelectList(_context.DmKhuVucs, "IdKhuVuc", "KhuVuc", tbDuLieuTrungTuyen.IdKhuVuc);
                return View(tbDuLieuTrungTuyen);
            }
            catch (Exception ex)
            {// bắt lỗi ko mong muốn và trả về lỗi tổng quát
                return BadRequest();
            }
        }
        // GET: TbDuLieuTrungTuyens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbDuLieuTrungTuyen = await _context.TbDuLieuTrungTuyens.FindAsync(id);
                if (tbDuLieuTrungTuyen == null)
                {
                    return NotFound();
                }
                ViewData["IdDoiTuongDauVao"] = new SelectList(_context.DmDoiTuongDauVaos, "IdDoiTuongDauVao", "DoiTuongDauVao", tbDuLieuTrungTuyen.IdDoiTuongDauVao);
                ViewData["IdDoiTuongUuTien"] = new SelectList(_context.DmDoiTuongUuTiens, "IdDoiTuongUuTien", "DoiTuongUuTien", tbDuLieuTrungTuyen.IdDoiTuongUuTien);
                ViewData["IdHinhThucTuyenSinh"] = new SelectList(_context.DmHinhThucTuyenSinhs, "IdHinhThucTuyenSinh", "HinhThucTuyenSinh", tbDuLieuTrungTuyen.IdHinhThucTuyenSinh);
                ViewData["IdKhuVuc"] = new SelectList(_context.DmKhuVucs, "IdKhuVuc", "KhuVuc", tbDuLieuTrungTuyen.IdKhuVuc);
                return View(tbDuLieuTrungTuyen);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST: TbDuLieuTrungTuyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDuLieuTrungTuyen,Cccd,HoVaTen,MaTuyenSinh,KhoaDaoTaoTrungTuyen,IdDoiTuongDauVao,IdDoiTuongUuTien,IdHinhThucTuyenSinh,IdKhuVuc,TruongThpt,ToHopMonTrungTuyen,DiemMon1,DiemMon2,DiemMon3,DiemUuTien,TongDiemXetTuyen,SoQuyetDinhTrungTuyen,NgayBanHanhQuyetDinhTrungTuyen,ChuongTrinhDaoTaoDaTotNghiepTrinhDoDaiHoc,NganhDaTotNghiepTrinhDoDaiHoc,ChuongTrinhDaoTaoDaTotNghiepTrinhDoThacSi,NganhDaTotNghiepTrinhDoThacSi")] TbDuLieuTrungTuyen tbDuLieuTrungTuyen)
        {
            try
            {
                if (id != tbDuLieuTrungTuyen.IdDuLieuTrungTuyen)
                {
                    return NotFound();
                }
                //Ktra các trường cần thiết

                if (string.IsNullOrWhiteSpace(tbDuLieuTrungTuyen.HoVaTen))
                {
                    ModelState.AddModelError("HoVaTen", "Họ Tên không được để trống");

                }
                if (string.IsNullOrWhiteSpace(tbDuLieuTrungTuyen.MaTuyenSinh))
                {
                    ModelState.AddModelError("MaTuyenSinh", "Mã Tuyển Sinh không được để trống");

                }
                if (string.IsNullOrWhiteSpace(tbDuLieuTrungTuyen.KhoaDaoTaoTrungTuyen))
                {
                    ModelState.AddModelError("KhoaDaoTaoTrungTuyen", "Khoa Đào Tạo Trúng Tuyển không được để trống");

                }
                if (string.IsNullOrWhiteSpace(tbDuLieuTrungTuyen.TruongThpt))
                {
                    ModelState.AddModelError("TruongThpt", "Tên Trường THPT không được để trống");

                }
                if (string.IsNullOrWhiteSpace(tbDuLieuTrungTuyen.ToHopMonTrungTuyen))
                {
                    ModelState.AddModelError("ToHopMonTrungTuyen", "Tổ Hợp Môn Trúng Tuyển không được để trống");

                }
                if (tbDuLieuTrungTuyen.DiemMon1 == null)
                {
                    ModelState.AddModelError("DiemMon1", "Điểm Môn 1 không được để trống");

                }
                if (tbDuLieuTrungTuyen.DiemMon2 == null)
                {
                    ModelState.AddModelError("DiemMon2", "Điểm Môn 2 không được để trống");

                }
                if (tbDuLieuTrungTuyen.DiemMon3 == null)
                {
                    ModelState.AddModelError("DiemMon3", "Điểm Môn3 không được để trống");

                }
                if (tbDuLieuTrungTuyen.DiemUuTien == null)
                {
                    ModelState.AddModelError("DiemUuTien", "Điểm Ưu Tiên không được để trống");

                }
                if (ModelState.IsValid)
                {
                    // Tính tổng điểm xét tuyển
                    tbDuLieuTrungTuyen.TongDiemXetTuyen = tbDuLieuTrungTuyen.DiemMon1 + tbDuLieuTrungTuyen.DiemMon2 + tbDuLieuTrungTuyen.DiemMon3 + tbDuLieuTrungTuyen.DiemUuTien;
                    try
                    {
                        _context.Update(tbDuLieuTrungTuyen);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TbDuLieuTrungTuyenExists(tbDuLieuTrungTuyen.IdDuLieuTrungTuyen))
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
                ViewData["IdDoiTuongDauVao"] = new SelectList(_context.DmDoiTuongDauVaos, "IdDoiTuongDauVao", "DoiTuongDauVao", tbDuLieuTrungTuyen.IdDoiTuongDauVao);
                ViewData["IdDoiTuongUuTien"] = new SelectList(_context.DmDoiTuongUuTiens, "IdDoiTuongUuTien", "DoiTuongUuTien", tbDuLieuTrungTuyen.IdDoiTuongUuTien);
                ViewData["IdHinhThucTuyenSinh"] = new SelectList(_context.DmHinhThucTuyenSinhs, "IdHinhThucTuyenSinh", "HinhThucTuyenSinh", tbDuLieuTrungTuyen.IdHinhThucTuyenSinh);
                ViewData["IdKhuVuc"] = new SelectList(_context.DmKhuVucs, "IdKhuVuc", "KhuVuc", tbDuLieuTrungTuyen.IdKhuVuc);
                return View(tbDuLieuTrungTuyen);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: TbDuLieuTrungTuyens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbDuLieuTrungTuyen = await _context.TbDuLieuTrungTuyens
                    .Include(t => t.IdDoiTuongDauVaoNavigation)
                    .Include(t => t.IdDoiTuongUuTienNavigation)
                    .Include(t => t.IdHinhThucTuyenSinhNavigation)
                    .Include(t => t.IdKhuVucNavigation)
                    .FirstOrDefaultAsync(m => m.IdDuLieuTrungTuyen == id);
                if (tbDuLieuTrungTuyen == null)
                {
                    return NotFound();
                }

                return View(tbDuLieuTrungTuyen);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        // POST: TbDuLieuTrungTuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbDuLieuTrungTuyen = await _context.TbDuLieuTrungTuyens.FindAsync(id);
            if (tbDuLieuTrungTuyen != null)
            {
                _context.TbDuLieuTrungTuyens.Remove(tbDuLieuTrungTuyen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbDuLieuTrungTuyenExists(int id)
        {
            return _context.TbDuLieuTrungTuyens.Any(e => e.IdDuLieuTrungTuyen == id);
        }
    }
}