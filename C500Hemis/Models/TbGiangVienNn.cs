using System;
using System.Collections.Generic;
using System.ComponentModel;
using C500Hemis.Models.DM;

namespace C500Hemis.Models;

public partial class TbGiangVienNn
{
    public int IdGvnn { get; set; }

    public int? IdCanBo { get; set; }

    [DisplayName (displayName: "Cơ quan chủ quản nước ngoài")]
    public string? CoQuanChuQuanOnuocNgoai { get; set; }

    public int? IdNoiDungHoatDongTaiVietNam { get; set; }

    public virtual TbCanBo? IdCanBoNavigation { get; set; }

    public virtual DmNoiDungHoatDongTaiVietNam? IdNoiDungHoatDongTaiVietNamNavigation { get; set; }
}
