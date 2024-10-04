using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using C500Hemis.Models.DM;
using Newtonsoft.Json.Serialization;

namespace C500Hemis.Models;

public partial class TbHopDong
{
    [DisplayName(displayName: "Mã số hợp đồng")]
    public int IdHopDong { get; set; }
    [DisplayName(displayName: "Mã số cán bộ")]
    public int? IdCanBo { get; set; }
    [DisplayName(displayName: "Số hợp đồng")]
    public string? SoHopDong { get; set; }
    [DisplayName(displayName: "Mã số loại hợp đồng")]
    public int? IdLoaiHopDong { get; set; }
    [DisplayName(displayName: "Số quyết định")]
    public string? SoQuyetDinh { get; set; }
    [DisplayName(displayName: "Ngày quyết định")]
    [Required(ErrorMessage = "Nhập đủ ngày tháng năm")]
    public DateOnly? NgayQuyetDinh { get; set; }
    [Required(ErrorMessage = "Chỉ nhập ngày")]
    [DisplayName(displayName: "Nhập đủ ngày tháng năm")]
    public DateOnly? CoGiaTriTu { get; set; }
    [Required(ErrorMessage = "Chỉ nhập ngày")]
    [DisplayName(displayName: "Nhập đủ ngày tháng năm")]
    public DateOnly? CoGiaTriDen { get; set; }
    [DisplayName(displayName: "Tình trạng hợp đồng")]
    public int? IdTinhTrangHopDong { get; set; }
    [DisplayName(displayName: "Làm việc toàn thời gian")]
    public bool? LamViecToanThoiGian { get; set; }
    [DisplayName(displayName: "Cán bộ")]
    public virtual TbCanBo? IdCanBoNavigation { get; set; }
    [DisplayName(displayName: "Loại hợp đồng")]
    public virtual DmLoaiHopDong? IdLoaiHopDongNavigation { get; set; }
    [DisplayName(displayName: "Tình trạng hợp đồng")]
    public virtual DmTinhTrangHopDong? IdTinhTrangHopDongNavigation { get; set; }
}
