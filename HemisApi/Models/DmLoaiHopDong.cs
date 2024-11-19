using System;
using System.Collections.Generic;

namespace C500Hemis;

public partial class DmLoaiHopDong
{
    public int IdLoaiHopDong { get; set; }

    public string? LoaiHopDong { get; set; }

    public virtual ICollection<TbHopDong> TbHopDongs { get; set; } = new List<TbHopDong>();
}
