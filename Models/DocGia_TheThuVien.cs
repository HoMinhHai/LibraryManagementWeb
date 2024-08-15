using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DoAnKiSu_ThuVien.Models
{
    public class DocGia_TheThuVien
    {
        public string hinhanh { get; set; }
        public string MaDocGia { get; set; }
        public string doiTuong { get; set; }
        public string TenDocGia { get; set; }
        public string DonVi { get; set; }
        public string NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string CCCD { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public int KhoaHoc { get; set; }
        public int NguoiThucHien { get; set; }
        public string NgayTao { get; set; }
        public string NgayHetHan { get; set; }
        public string MatKhau { get; set; }
       
    }
}