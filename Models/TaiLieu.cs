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
    
    public partial class TaiLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiLieu()
        {
            this.PhienBanTaiLieux = new HashSet<PhienBanTaiLieu>();
            this.TacGias = new HashSet<TacGia>();
        }
    
        public int ID_TL { get; set; }
        public string TenTaiLieu { get; set; }
        public string KyHieuPhanLoai { get; set; }
        public Nullable<int> LoaiTaiLieu { get; set; }
        public Nullable<int> ChuDe { get; set; }
        public Nullable<System.DateTime> NgayNhap { get; set; }
    
        public virtual ChuDe ChuDe1 { get; set; }
        public virtual LoaiTaiLieu LoaiTaiLieu1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhienBanTaiLieu> PhienBanTaiLieux { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TacGia> TacGias { get; set; }
    }
}
