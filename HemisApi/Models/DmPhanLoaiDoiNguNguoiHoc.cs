using System;
using System.Collections.Generic;

namespace C500Hemis;

public partial class DmPhanLoaiDoiNguNguoiHoc
{
    public int IdPhanLoaiDoiNguNguoiHoc { get; set; }

    public string? PhanLoaiDoiNguNguoiHoc { get; set; }

    public virtual ICollection<TbDoiTuongThamGium> TbDoiTuongThamGia { get; set; } = new List<TbDoiTuongThamGium>();
}
