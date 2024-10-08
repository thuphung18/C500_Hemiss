using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using C500Hemis.Models.DM;

namespace C500Hemis.Models;

public partial class TbThongTinViPham
{
    [Display(Name = "Thông tin vi phạm")]

    public int IdThongTinViPham { get; set; }

    [Display(Name = "Mã học viên")]
    public int? IdHocVien { get; set; }

    [Display(Name = "Thời gian vi phạm")]

    public DateOnly? ThoiGianViPham { get; set; }

    [Display(Name = "Nội dung vi phạm")]
    public string? NoiDungViPham { get; set; }

    [Display(Name = "Hình thức xử lí")]

    public string? HinhThucXuLy { get; set; }

    [Display(Name = "Loại vi phạm")]
    public int? IdLoaiViPham { get; set; }

    [Display(Name = "Mã học viên")]
    public virtual TbHocVien? IdHocVienNavigation { get; set; }

    [Display(Name = "Loại vi phạm")]
    public virtual DmLoaiViPham? IdLoaiViPhamNavigation { get; set; }
}
