using System;
using System.Collections.Generic;

namespace C500Hemis;

public partial class DmHinhThucBoNhiem
{
    public int IdHinhThucBoNhiem { get; set; }

    public string? HinhThucBoNhiem { get; set; }

    public virtual ICollection<TbDonViCongTacCuaCanBo> TbDonViCongTacCuaCanBos { get; set; } = new List<TbDonViCongTacCuaCanBo>();
}
