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
            // Thêm thủ công giá trị cho IdDoiTuongDauVao
            var doiTuongDauVaoList = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Tốt nghiệp Trung học phổ thông" },
        new SelectListItem { Value = "2", Text = "2 - Tốt nghiệp Trung cấp" },
        new SelectListItem { Value = "3", Text = "3 - Tốt nghiệp Cao đẳng" },
        new SelectListItem { Value = "4", Text = "4 - Tốt nghiệp Đại học" },
        new SelectListItem { Value = "5", Text = "5 - Thạc sĩ" },
        new SelectListItem { Value = "6", Text = "6 - Tiến sĩ" },
        new SelectListItem { Value = "7", Text = "7 - Cử tuyển" },
        new SelectListItem { Value = "8", Text = "8 - Dự bị" },
        new SelectListItem { Value = "9", Text = "9 - Tuyển thẳng" }
    };
            ViewData["IdDoiTuongDauVao"] = new SelectList(doiTuongDauVaoList, "Value", "Text");

            // Thêm thủ công giá trị cho IdDoiTuongUuTien
            var doiTuongUuTienList = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Nhóm ưu tiên 1" },
        new SelectListItem { Value = "2", Text = "2 - Nhóm ưu tiên 2" }
        // Thêm các giá trị khác nếu cần
    };
            ViewData["IdDoiTuongUuTien"] = new SelectList(doiTuongUuTienList, "Value", "Text");

            // Thêm thủ công giá trị cho IdHinhThucTuyenSinh
            var hinhThucTuyenSinhList = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Tuyển sinh theo hình thức khác" },
        new SelectListItem { Value = "2", Text = "2 - Tuyển sinh xét theo kết quả thi THPT" },
        new SelectListItem { Value = "3", Text = "3 - Tuyển sinh xét học bạ" },
        new SelectListItem { Value = "4", Text = "4 - Tuyển thẳng" }
        // Thêm các giá trị khác nếu cần
    };
            ViewData["IdHinhThucTuyenSinh"] = new SelectList(hinhThucTuyenSinhList, "Value", "Text");

            // Thêm thủ công giá trị cho IdKhuVuc
            var khuVucList = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Khu vực 1" },
        new SelectListItem { Value = "2", Text = "2 - Khu vực 2" },
        new SelectListItem { Value = "3", Text = "3 - Khu vực 2NT" },
        new SelectListItem { Value = "4", Text = "4 - Khu vực 3" }
        // Thêm các giá trị khác nếu cần
    };
            ViewData["IdKhuVuc"] = new SelectList(khuVucList, "Value", "Text");

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
            // Nếu ModelState không hợp lệ, gán thủ công giá trị cho dropdowns

            // Gán thủ công giá trị cho IdDoiTuongDauVao
            ViewData["IdDoiTuongDauVao"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Tốt nghiệp Trung học phổ thông" },
        new SelectListItem { Value = "2", Text = "2 - Tốt nghiệp Trung cấp" },
        new SelectListItem { Value = "3", Text = "3 - Tốt nghiệp Cao đẳng" },
        new SelectListItem { Value = "4", Text = "4 - Tốt nghiệp Đại học" },
        new SelectListItem { Value = "5", Text = "5 - Thạc sĩ" },
        new SelectListItem { Value = "6", Text = "6 - Tiến sĩ" },
        new SelectListItem { Value = "7", Text = "7 - Cử tuyển" },
        new SelectListItem { Value = "8", Text = "8 - Dự bị" },
        new SelectListItem { Value = "9", Text = "9 - Tuyển thẳng" }
    }, "Value", "Text", tbDuLieuTrungTuyen.IdDoiTuongDauVao);

            // Gán thủ công giá trị cho IdDoiTuongUuTien
            ViewData["IdDoiTuongUuTien"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Nhóm ưu tiên 1" },
        new SelectListItem { Value = "2", Text = "2 - Nhóm ưu tiên 2" }
        // Thêm các giá trị khác nếu cần
    }, "Value", "Text", tbDuLieuTrungTuyen.IdDoiTuongUuTien);

            // Gán thủ công giá trị cho IdHinhThucTuyenSinh
            ViewData["IdHinhThucTuyenSinh"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Tuyển sinh theo hình thức khác" },
        new SelectListItem { Value = "2", Text = "2 - Tuyển sinh xét theo kết quả thi THPT" },
        new SelectListItem { Value = "3", Text = "3 - Tuyển sinh xét học bạ" },
        new SelectListItem { Value = "4", Text = "4 - Tuyển thẳng" }
        // Thêm các giá trị khác nếu cần
    }, "Value", "Text", tbDuLieuTrungTuyen.IdHinhThucTuyenSinh);

            // Gán thủ công giá trị cho IdKhuVuc
            ViewData["IdKhuVuc"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Khu vực 1" },
        new SelectListItem { Value = "2", Text = "2 - Khu vực 2" },
        new SelectListItem { Value = "3", Text = "3 - Khu vực 2NT" },
        new SelectListItem { Value = "4", Text = "4 - Khu vực 3" }
        // Thêm các giá trị khác nếu cần
    }, "Value", "Text", tbDuLieuTrungTuyen.IdKhuVuc);

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

            // Gán thủ công giá trị cho IdDoiTuongDauVao
            ViewData["IdDoiTuongDauVao"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Tốt nghiệp Trung học phổ thông" },
        new SelectListItem { Value = "2", Text = "2 - Tốt nghiệp Trung cấp" },
        new SelectListItem { Value = "3", Text = "3 - Tốt nghiệp Cao đẳng" },
        new SelectListItem { Value = "4", Text = "4 - Tốt nghiệp Đại học" },
        new SelectListItem { Value = "5", Text = "5 - Thạc sĩ" },
        new SelectListItem { Value = "6", Text = "6 - Tiến sĩ" },
        new SelectListItem { Value = "7", Text = "7 - Cử tuyển" },
        new SelectListItem { Value = "8", Text = "8 - Dự bị" },
        new SelectListItem { Value = "9", Text = "9 - Tuyển thẳng" }
    }, "Value", "Text", tbDuLieuTrungTuyen.IdDoiTuongDauVao);

            // Gán thủ công giá trị cho IdDoiTuongUuTien
            ViewData["IdDoiTuongUuTien"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Nhóm ưu tiên 1" },
        new SelectListItem { Value = "2", Text = "2 - Nhóm ưu tiên 2" }
        // Thêm các giá trị khác nếu cần
    }, "Value", "Text", tbDuLieuTrungTuyen.IdDoiTuongUuTien);

            // Gán thủ công giá trị cho IdHinhThucTuyenSinh
            ViewData["IdHinhThucTuyenSinh"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Tuyển sinh theo hình thức khác" },
        new SelectListItem { Value = "2", Text = "2 - Tuyển sinh xét theo kết quả thi THPT" },
        new SelectListItem { Value = "3", Text = "3 - Tuyển sinh xét học bạ" },
        new SelectListItem { Value = "4", Text = "4 - Tuyển thẳng" }
        // Thêm các giá trị khác nếu cần
    }, "Value", "Text", tbDuLieuTrungTuyen.IdHinhThucTuyenSinh);

            // Gán thủ công giá trị cho IdKhuVuc
            ViewData["IdKhuVuc"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Khu vực 1" },
        new SelectListItem { Value = "2", Text = "2 - Khu vực 2" },
        new SelectListItem { Value = "3", Text = "3 - Khu vực 2NT" },
        new SelectListItem { Value = "4", Text = "4 - Khu vực 3" }
        // Thêm các giá trị khác nếu cần
    }, "Value", "Text", tbDuLieuTrungTuyen.IdKhuVuc);

            return View(tbDuLieuTrungTuyen);
        }


        // POST: TbDuLieuTrungTuyens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDuLieuTrungTuyen,Cccd,HoVaTen,MaTuyenSinh,KhoaDaoTaoTrungTuyen,IdDoiTuongDauVao,IdDoiTuongUuTien,IdHinhThucTuyenSinh,IdKhuVuc,TruongThpt,ToHopMonTrungTuyen,DiemMon1,DiemMon2,DiemMon3,DiemUuTien,SoQuyetDinhTrungTuyen,NgayBanHanhQuyetDinhTrungTuyen,ChuongTrinhDaoTaoDaTotNghiepTrinhDoDaiHoc,NganhDaTotNghiepTrinhDoDaiHoc,ChuongTrinhDaoTaoDaTotNghiepTrinhDoThacSi,NganhDaTotNghiepTrinhDoThacSi")] TbDuLieuTrungTuyen tbDuLieuTrungTuyen)
        {
            if (id != tbDuLieuTrungTuyen.IdDuLieuTrungTuyen)
            {
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
            // Nếu ModelState không hợp lệ, hiển thị lại form cùng với các dropdown đã chọn trước đó
            // Gán thủ công giá trị cho IdDoiTuongDauVao
            ViewData["IdDoiTuongDauVao"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Tốt nghiệp Trung học phổ thông" },
        new SelectListItem { Value = "2", Text = "2 - Tốt nghiệp Trung cấp" },
        new SelectListItem { Value = "3", Text = "3 - Tốt nghiệp Cao đẳng" },
        new SelectListItem { Value = "4", Text = "4 - Tốt nghiệp Đại học" },
        new SelectListItem { Value = "5", Text = "5 - Thạc sĩ" },
        new SelectListItem { Value = "6", Text = "6 - Tiến sĩ" },
        new SelectListItem { Value = "7", Text = "7 - Cử tuyển" },
        new SelectListItem { Value = "8", Text = "8 - Dự bị" },
        new SelectListItem { Value = "9", Text = "9 - Tuyển thẳng" }
    }, "Value", "Text", tbDuLieuTrungTuyen.IdDoiTuongDauVao);

            // Gán thủ công giá trị cho IdDoiTuongUuTien
            ViewData["IdDoiTuongUuTien"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Nhóm ưu tiên 1" },
        new SelectListItem { Value = "2", Text = "2 - Nhóm ưu tiên 2" }
        // Thêm các giá trị khác nếu cần
    }, "Value", "Text", tbDuLieuTrungTuyen.IdDoiTuongUuTien);

            // Gán thủ công giá trị cho IdHinhThucTuyenSinh
            ViewData["IdHinhThucTuyenSinh"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Tuyển sinh theo hình thức khác" },
        new SelectListItem { Value = "2", Text = "2 - Tuyển sinh xét theo kết quả thi THPT" },
        new SelectListItem { Value = "3", Text = "3 - Tuyển sinh xét học bạ" },
        new SelectListItem { Value = "4", Text = "4 - Tuyển thẳng" }
        // Thêm các giá trị khác nếu cần
    }, "Value", "Text", tbDuLieuTrungTuyen.IdHinhThucTuyenSinh);

            // Gán thủ công giá trị cho IdKhuVuc
            ViewData["IdKhuVuc"] = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "1 - Khu vực 1" },
        new SelectListItem { Value = "2", Text = "2 - Khu vực 2" },
        new SelectListItem { Value = "3", Text = "3 - Khu vực 2NT" },
        new SelectListItem { Value = "4", Text = "4 - Khu vực 3" }
        // Thêm các giá trị khác nếu cần
    }, "Value", "Text", tbDuLieuTrungTuyen.IdKhuVuc);

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