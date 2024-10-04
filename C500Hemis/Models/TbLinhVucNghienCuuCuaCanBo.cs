using System;
using System.Collections.Generic;
using System.ComponentModel;
using C500Hemis.Models.DM;

namespace C500Hemis.Models;

public partial class TbLinhVucNghienCuuCuaCanBo
{
    public int IdLinhVucNghienCuuCuaCanBo { get; set; }

    public int? IdCanBo { get; set; }

    public int? IdLinhVucNghienCuu { get; set; }

    [DisplayName (displayName: "Lĩnh vực nghiên cứu chuyên sâu")]
    public bool? LaLinhVucNghienCuuChuyenSau { get; set; }

    [DisplayName (displayName: "Số năm nghiên cứu")]
    public int? SoNamNghienCuu { get; set; }

    [DisplayName (displayName: "Mã số Id của cán bộ")]
    public virtual TbCanBo? IdCanBoNavigation { get; set; }

    [DisplayName (displayName: "Mã số Id của lĩnh vực nghiên cứu")]
    public virtual DmLinhVucNghienCuu? IdLinhVucNghienCuuNavigation { get; set; }
}
