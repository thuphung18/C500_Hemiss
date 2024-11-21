using System;
using System.Collections.Generic;

namespace C500Hemis;

public partial class DmVaiTroThamGia
{
    public int IdVaiTroThamGia { get; set; }

    public string? VaiTroThamGia { get; set; }

    public virtual ICollection<TbDoiTuongThamGium> TbDoiTuongThamGia { get; set; } = new List<TbDoiTuongThamGium>();

    public virtual ICollection<TbThanhPhanThamGiaDoanCongTac> TbThanhPhanThamGiaDoanCongTacs { get; set; } = new List<TbThanhPhanThamGiaDoanCongTac>();
}
