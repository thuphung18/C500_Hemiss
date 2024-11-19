using System;
using System.Collections.Generic;

namespace C500Hemis;

public partial class DmLoaiBoiDuong
{
    public int IdLoaiBoiDuong { get; set; }

    public string? LoaiBoiDuong { get; set; }

    public virtual ICollection<TbKhoaBoiDuongTapHuanThamGiaCuaCanBo> TbKhoaBoiDuongTapHuanThamGiaCuaCanBos { get; set; } = new List<TbKhoaBoiDuongTapHuanThamGiaCuaCanBo>();
}
