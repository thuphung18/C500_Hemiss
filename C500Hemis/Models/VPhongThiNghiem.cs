using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace C500Hemis.Models;

public partial class VPhongThiNghiem
{
    [DisplayName("Mã số công trình")]
    
    public string? MaCongTrinh { get; set; }
    [DisplayName("Loại phòng thí nghiệm")]

    public string? LoaiPhongThiNghiem { get; set; }
    [DisplayName("Lĩnh vực nghiên cứu")]
    public string? LinhVucNghienCuu { get; set; }
    [DisplayName("Mức độ đáp ứng nhu cầu NCKH")]

    public string? MucDoDapUngNhuCauNckh { get; set; }
    [DisplayName("Năm đưa vào sử dụng")]
    public string? NamDuaVaoSuDung { get; set; }
}
