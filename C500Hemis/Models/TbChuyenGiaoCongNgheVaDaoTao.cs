using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using C500Hemis.Models.DM;

namespace C500Hemis.Models;

public partial class TbChuyenGiaoCongNgheVaDaoTao
{
    //[DisplayName(displayName: "NỘI DUNG HOẠT ĐỘNG TẠI VIỆT NAM")]
    [Display(Name="Id Chuyển Giao Công Nghệ Và Đào Tạo")]
    public int IdChuyenGiaoCongNgheVaDaoTao { get; set; }
    [Display(Name = "Id Nhiệm vụ KHCN")]

    public int? IdNhiemVuKhcn { get; set; }
    [Display(Name = "Mã số hợp đồng")]
    [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Mã Số Hợp Đồng chỉ được chứa ký tự chữ và số.")]
   // [Required(AllowEmptyStrings = false, ErrorMessage = "Không được để trống.")]
    public string? MaSoHopDong { get; set; }


    [Display(Name = "Tên")]
    //[RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Tên chỉ được chứa ký tự chữ .")]
    [RegularExpression(@"^[a-zA-Zàáảãạâầấậẩẫăằắặẳẵèéẻẽẹêềếệểễìíỉĩịòóỏõọôồốộổỗơờớởỡùúủũụưừứựửữỳýÿ\s]+$", ErrorMessage = "Tên chỉ được chứa ký tự chữ và dấu cách.")]

    public string? Ten { get; set; }


    [Display(Name = "Tổng chi phí thực hiện")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = " Tổng chi phí thực hiện chỉ được chứa ký tự số.")]
    public int? TongChiPhiThucHien { get; set; }


    [Display(Name = "Tổng chi phí thời gian thực hiện")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "Tổng chi phí thời gian thực hiện chỉ được chứa ký tự số.")]
    public int? TongThoiGianThucHien { get; set; }

    public int? IdHinhThucChuyenGiaoCongNghe { get; set; }


    [Display(Name = "Phương thức chuyển giao công nghệ")]
    // [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = " Chỉ được chứa ký tự chữ .")]
    [RegularExpression(@"^[a-zA-Zàáảãạâầấậẩẫăằắặẳẵèéẻẽẹêềếệểễìíỉĩịòóỏõọôồốộổỗơờớởỡùúủũụưừứựửữỳýÿ\s]+$", ErrorMessage = "Chỉ được chứa ký tự chữ và dấu cách.")]
    public string? PhuongThucChuyenGiaoCongNghe { get; set; }

    [Display(Name = "Chủ sở hữu")]
    // [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = " Chỉ được chứa ký tự chữ .")]
    [RegularExpression(@"^[a-zA-Zàáảãạâầấậẩẫăằắặẳẵèéẻẽẹêềếệểễìíỉĩịòóỏõọôồốộổỗơờớởỡùúủũụưừứựửữỳýÿ\s]+$", ErrorMessage = "Chỉ được chứa ký tự chữ và dấu cách.")]

    public string? ChuSoHuu { get; set; }

    [Display(Name = "Đơn vị chủ trì")]
    // [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = " Chỉ được chứa ký tự chữ  .")]
    [RegularExpression(@"^[a-zA-Zàáảãạâầấậẩẫăằắặẳẵèéẻẽẹêềếệểễìíỉĩịòóỏõọôồốộổỗơờớởỡùúủũụưừứựửữỳýÿ\s]+$", ErrorMessage = "Chỉ được chứa ký tự chữ và dấu cách.")]

    public string? DonViChuTri { get; set; }

    [Display(Name = "Đơn vị phối hợp")]
    //  [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = " Chỉ được chứa ký tự chữ .")]
    [RegularExpression(@"^[a-zA-Zàáảãạâầấậẩẫăằắặẳẵèéẻẽẹêềếệểễìíỉĩịòóỏõọôồốộổỗơờớởỡùúủũụưừứựửữỳýÿ\s]+$", ErrorMessage = "Chỉ được chứa ký tự chữ và dấu cách.")]

    public string? DonViPhoiHop { get; set; }

    [Display(Name = "Đơn vị Nhận Chuyển Giao")]
    //  [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = " Chỉ được chứa ký tự chữ .")]
    [RegularExpression(@"^[a-zA-Zàáảãạâầấậẩẫăằắặẳẵèéẻẽẹêềếệểễìíỉĩịòóỏõọôồốộổỗơờớởỡùúủũụưừứựửữỳýÿ\s]+$", ErrorMessage = "Chỉ được chứa ký tự chữ và dấu cách.")]

    public string? DonViNhanChuyenGiao { get; set; }

    [Display(Name = "Tóm Tắt")]
    // [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = " Chỉ được chứa ký tự chữ .")]
    [RegularExpression(@"^[a-zA-Zàáảãạâầấậẩẫăằắặẳẵèéẻẽẹêềếệểễìíỉĩịòóỏõọôồốộổỗơờớởỡùúủũụưừứựửữỳýÿ\s]+$", ErrorMessage = "Chỉ được chứa ký tự chữ và dấu cách.")]

    public string? TomTat { get; set; }

    [Display(Name = "Tên Dự Án")]
    [RegularExpression(@"^[a-zA-Zàáảãạâầấậẩẫăằắặẳẵèéẻẽẹêềếệểễìíỉĩịòóỏõọôồốộổỗơờớởỡùúủũụưừứựửữỳýÿ\s]+$", ErrorMessage = "Tên chỉ được chứa ký tự chữ và dấu cách.")]

    public string? TenDuAn { get; set; }

    [Display(Name = "Giá trị Hợp Đồng")]
   // [Range(1, int.MaxValue, ErrorMessage = "Giá trị phải là một số nguyên dương.")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "Chỉ được chứa ký tự số.")]
    public int? GiaTriHopDong { get; set; }

    public int? IdNganhKinhTe { get; set; }

    public int? IdTrangThaiHopDong { get; set; }

    [Display(Name = "Số người được đào tạo chuyển giao CN")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "Chỉ được chứa ký tự số.")]
    public int? SoNguoiDuocDaoTaoChuyenGiaoCn { get; set; }

    public int? IdLinhVucNghienCuu { get; set; }

    public virtual DmHinhThucChuyenGiaoCongNghe? IdHinhThucChuyenGiaoCongNgheNavigation { get; set; }

    public virtual DmLinhVucNghienCuu? IdLinhVucNghienCuuNavigation { get; set; }

    public virtual DmNganhKinhTe? IdNganhKinhTeNavigation { get; set; }

    public virtual TbNhiemVuKhcn? IdNhiemVuKhcnNavigation { get; set; }

    public virtual DmTrangThaiHopDong? IdTrangThaiHopDongNavigation { get; set; }
}
