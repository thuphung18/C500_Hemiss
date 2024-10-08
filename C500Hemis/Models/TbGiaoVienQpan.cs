using System;
using System.Collections.Generic;
using System.ComponentModel;
using C500Hemis.Models.DM;

namespace C500Hemis.Models;

public partial class TbGiaoVienQpan
{
    [DisplayName(displayName:"Id giáo viên")]
    public int IdGiaoVienQpan { get; set; }
    [DisplayName(displayName: "Id cán bộ")]
    public int? IdCanBo { get; set; }
    [DisplayName(displayName: "Năm bắt đầu biệt phái")]
    public DateOnly? NamBatDauBietPhai { get; set; }
    [DisplayName(displayName: "Số năm biệt phái")]
    public DateOnly? SoNamBietPhai { get; set; }
    [DisplayName(displayName: "Id loại giảng viên")]
    public int? IdLoaiGiangVienQp { get; set; }
    [DisplayName(displayName: "Đào tạo gdqpan")]
    public string? DaoTaoGdqpan { get; set; }
    [DisplayName(displayName: "Id quân hàm")]
    public int? IdQuanHam { get; set; }
    [DisplayName(displayName: "Sở trường công tác")]
    public string? SoTruongCongTac { get; set; }

    public virtual TbCanBo? IdCanBoNavigation { get; set; }

    public virtual DmLoaiGiangVienQuocPhong? IdLoaiGiangVienQpNavigation { get; set; }

    public virtual DmQuanHam? IdQuanHamNavigation { get; set; }
}
