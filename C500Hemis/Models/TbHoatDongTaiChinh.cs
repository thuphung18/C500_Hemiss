using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using C500Hemis.Models.DM;

namespace C500Hemis.Models;

public partial class TbHoatDongTaiChinh
{
    [Required(ErrorMessage = "Bắt buộc nhập")]
    public int IdHoatDongTaiChinh { get; set; }

    [DisplayName(displayName: "ID Loại hoạt động tài chính")]
    public int? IdLoaiHoatDongTaiChinh { get; set; }

    [DisplayName(displayName: "Năm tài chính")]
    [DisplayFormat(DataFormatString = "{0:yyyy}")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage ="Nhập lại năm!")]
    public string? NamTaiChinh { get; set; }

    [DisplayName(displayName: "Kinh phí")]
    public int? KinhPhi { get; set; }

    [DisplayName(displayName: "Nội dung")]
    public string? NoiDung { get; set; }

    [DisplayName(displayName: "ID Loại hoạt động điều hướng tài chính")]
    public virtual DmHoatDongTaiChinh? IdLoaiHoatDongTaiChinhNavigation { get; set; }
}
