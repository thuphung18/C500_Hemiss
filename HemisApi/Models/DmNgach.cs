using System;
using System.Collections.Generic;

namespace C500Hemis;

public partial class DmNgach
{
    public int IdNgach { get; set; }

    public string? Ngach { get; set; }

    public virtual ICollection<TbCanBo> TbCanBos { get; set; } = new List<TbCanBo>();
}
