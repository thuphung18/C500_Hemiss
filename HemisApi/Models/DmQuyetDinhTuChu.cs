using System;
using System.Collections.Generic;

namespace C500Hemis;

public partial class DmQuyetDinhTuChu
{
    public int IdQuyetDinhTuChu { get; set; }

    public string? QuyetDinhTuChu { get; set; }

    public virtual ICollection<TbDanhSachNganhDaoTao> TbDanhSachNganhDaoTaos { get; set; } = new List<TbDanhSachNganhDaoTao>();
}
