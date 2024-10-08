//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnKiSu_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            this.PhieuMuons = new HashSet<PhieuMuon>();
            this.TheThuViens = new HashSet<TheThuVien>();
        }
    
        public int ID_NV { get; set; }
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string ChucVu { get; set; }
        public string CCCD { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> NgayVaoLam { get; set; }
        public string DiaChi { get; set; }
        public string TrinhDo { get; set; }
        public string GhiChu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }
        public virtual TaiKhoanNoiBo TaiKhoanNoiBo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TheThuVien> TheThuViens { get; set; }
    }
}
