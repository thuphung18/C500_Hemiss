using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using C500Hemis.Models.DM;

namespace C500Hemis.Models;

public partial class TbQuaTrinhCongTacCuaCanBo
{
    public int IdQuaTrinhCongTacCuaCanBo { get; set; }

    public int? IdCanBo { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateOnly? TuThangNam { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateOnly? DenThangNam { get; set; }

    public int? IdChucVu { get; set; }

    public int? IdChucDanhGiangVien { get; set; }

    public string? DonViCongTac { get; set; }

    public virtual TbCanBo? IdCanBoNavigation { get; set; }

    public virtual DmChucDanhGiangVien? IdChucDanhGiangVienNavigation { get; set; }

    public virtual DmChucVu? IdChucVuNavigation { get; set; }
}
