using System;
using System.Collections.Generic;
using C500Hemis.Models.DM;

namespace C500Hemis.Models;

public partial class TbNganhDungTenGiangDay
{
    public int IdNganhDungTenGiangDay { get; set; }

    public int? IdCanBo { get; set; }

    public int? IdNganhDaoTao { get; set; }

    public double? TrongSo { get; set; }

    public virtual TbCanBo? IdCanBoNavigation { get; set; }

    public virtual DmNganhDaoTao? IdNganhDaoTaoNavigation { get; set; }
}
