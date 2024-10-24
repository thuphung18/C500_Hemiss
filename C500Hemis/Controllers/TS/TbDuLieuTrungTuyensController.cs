using C500Hemis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: TbDuLieuTrungTuyens/Create
        public IActionResult Create()
        {
            // Dùng Dm có sẵn của IdDoiTuongDauVao để lấy dữ liệu sau đó dùng id truy xuất dữ liệu hiển thị ra bảng chọn
            ViewData["IdDoiTuongDauVao"] = new SelectList(_context.DmDoiTuongDauVaos, "IdDoiTuongDauVao", "DoiTuongDauVao");
            // DmDoiTuongUuTiens
            ViewData["IdDoiTuongUuTien"] = new SelectList(_context.DmDoiTuongUuTiens, "IdDoiTuongUuTien", "DoiTuongUuTien");
            // DmHinhThucTuyenSinh
            ViewData["IdHinhThucTuyenSinh"] = new SelectList(_context.DmHinhThucTuyenSinhs, "IdHinhThucTuyenSinh", "HinhThucTuyenSinh");
            // DmKhuVuc
            ViewData["IdKhuvuc"] = new SelectList(_context.DmKhuVucs, "IdKhuVuc", "KhuVuc");
            return View();
        }
        // POST: TbDuLieuTrungTuyens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDuLieuTrungTuyen,Cccd,HoVaTen,MaTuyenSinh,KhoaDaoTaoTrungTuyen,IdDoiTuongDauVao,IdDoiTuongUuTien,IdHinhThucTuyenSinh,IdKhuVuc,TruongThpt,ToHopMonTrungTuyen,DiemMon1,DiemMon2,DiemMon3,DiemUuTien,SoQuyetDinhTrungTuyen,NgayBanHanhQuyetDinhTrungTuyen,ChuongTrinhDaoTaoDaTotNghiepTrinhDoDaiHoc,NganhDaTotNghiepTrinhDoDaiHoc,ChuongTrinhDaoTaoDaTotNghiepTrinhDoThacSi,NganhDaTotNghiepTrinhDoThacSi")] TbDuLieuTrungTuyen tbDuLieuTrungTuyen)
        {
            if (ModelState.IsValid)
            {
                // Tính tổng điểm xét tuyển
                tbDuLieuTrungTuyen.TongDiemXetTuyen = tbDuLieuTrungTuyen.DiemMon1 + tbDuLieuTrungTuyen.DiemMon2 + tbDuLieuTrungTuyen.DiemMon3 + tbDuLieuTrungTuyen.DiemUuTien;

                _context.Add(tbDuLieuTrungTuyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Sử dụng Dm có sẵn của đối tượng đầu vào
            ViewData["IdDoiTuongDauVao"] = new SelectList(_context.DmDoiTuongDauVaos, "IdDoiTuongDauVao", "DoiTuongDauVao", tbDuLieuTrungTuyen.IdDoiTuongDauVao);
            //
            ViewData["IdDoiTuongUuTien"] = new SelectList(_context.DmDoiTuongUuTiens, "IdDoiTuongUuTien", "DoiTuongUuTien", tbDuLieuTrungTuyen.IdDoiTuongUuTien);
            //
            ViewData["IdHinhThucTuyenSinh"] = new SelectList(_context.DmHinhThucTuyenSinhs, "IdHinhThucTuyenSinh", "HinhThucTuyenSinh", tbDuLieuTrungTuyen.IdHinhThucTuyenSinh);
            //
            ViewData["IdKhuVuc"] = new SelectList(_context.DmKhuVucs, "IdKhuVuc", "KhuVuc", tbDuLieuTrungTuyen.IdKhuVuc);
            return View(tbDuLieuTrungTuyen);
        }


        // GET: TbDuLieuTrungTuyens/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            // Dùng Dm có sẵn của IdDoiTuongDauVao để lấy dữ liệu sau đó dùng id truy xuất dữ liệu hiển thị ra bảng chọn
            ViewData["IdDoiTuongDauVao"] = new SelectList(_context.DmDoiTuongDauVaos, "IdDoiTuongDauVao", "DoiTuongDauVao");
            // DmDoiTuongUuTiens
            ViewData["IdDoiTuongUuTien"] = new SelectList(_context.DmDoiTuongUuTiens, "IdDoiTuongUuTien", "DoiTuongUuTien");
            // DmHinhThucTuyenSinh
            ViewData["IdHinhThucTuyenSinh"] = new SelectList(_context.DmHinhThucTuyenSinhs, "IdHinhThucTuyenSinh", "HinhThucTuyenSinh");
            // DmKhuVuc
            ViewData["IdKhuvuc"] = new SelectList(_context.DmKhuVucs, "IdKhuVuc", "KhuVuc");
            return View(tbDuLieuTrungTuyen);
        }

            // POST: TbDuLieuTrungTuyens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDuLieuTrungTuyen,Cccd,HoVaTen,MaTuyenSinh,KhoaDaoTaoTrungTuyen,IdDoiTuongDauVao,IdDoiTuongUuTien,IdHinhThucTuyenSinh,IdKhuVuc,TruongThpt,ToHopMonTrungTuyen,DiemMon1,DiemMon2,DiemMon3,DiemUuTien,SoQuyetDinhTrungTuyen,NgayBanHanhQuyetDinhTrungTuyen,ChuongTrinhDaoTaoDaTotNghiepTrinhDoDaiHoc,NganhDaTotNghiepTrinhDoDaiHoc,ChuongTrinhDaoTaoDaTotNghiepTrinhDoThacSi,NganhDaTotNghiepTrinhDoThacSi")] TbDuLieuTrungTuyen tbDuLieuTrungTuyen)
        {
            if (id != tbDuLieuTrungTuyen.IdDuLieuTrungTuyen)
            {
                Console.WriteLine(id);
                Console.WriteLine(tbDuLieuTrungTuyen.IdDuLieuTrungTuyen + " dwadadwa\n\n\n");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Tính tổng điểm xét tuyển
                    tbDuLieuTrungTuyen.TongDiemXetTuyen = tbDuLieuTrungTuyen.DiemMon1 + tbDuLieuTrungTuyen.DiemMon2 + tbDuLieuTrungTuyen.DiemMon3 + tbDuLieuTrungTuyen.DiemUuTien;

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

            // Sử dụng Dm có sẵn của đối tượng đầu vào
            ViewData["IdDoiTuongDauVao"] = new SelectList(_context.DmDoiTuongDauVaos, "IdDoiTuongDauVao", "DoiTuongDauVao", tbDuLieuTrungTuyen.IdDoiTuongDauVao);
            //
            ViewData["IdDoiTuongUuTien"] = new SelectList(_context.DmDoiTuongUuTiens, "IdDoiTuongUuTien", "DoiTuongUuTien", tbDuLieuTrungTuyen.IdDoiTuongUuTien);
            //
            ViewData["IdHinhThucTuyenSinh"] = new SelectList(_context.DmHinhThucTuyenSinhs, "IdHinhThucTuyenSinh", "HinhThucTuyenSinh", tbDuLieuTrungTuyen.IdHinhThucTuyenSinh);
            //
            ViewData["IdKhuVuc"] = new SelectList(_context.DmKhuVucs, "IdKhuVuc", "KhuVuc", tbDuLieuTrungTuyen.IdKhuVuc);
            return View(tbDuLieuTrungTuyen);
        }

        // GET: TbDuLieuTrungTuyens/Delete/5
        public async Task<IActionResult> Delete(int? id)
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