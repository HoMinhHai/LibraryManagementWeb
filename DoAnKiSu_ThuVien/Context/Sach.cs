//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnKiSu_ThuVien.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            this.BienLais = new HashSet<BienLai>();
            this.PhieuMuons = new HashSet<PhieuMuon>();
            this.ThongKes = new HashSet<ThongKe>();
        }
    
        public int id_sach { get; set; }
        public string ten_sach { get; set; }
        public Nullable<int> id_tac_gia { get; set; }
        public Nullable<int> id_loai_sach { get; set; }
        public Nullable<int> id_ngon_ngu { get; set; }
        public Nullable<int> id_NXB { get; set; }
        public Nullable<int> id_vi_tri { get; set; }
        public System.DateTime ngay_nhap { get; set; }
        public string tom_tat { get; set; }
        public string trang_thai { get; set; }
        public Nullable<int> so_luong_con { get; set; }
        public Nullable<double> price { get; set; }
        public string avatar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BienLai> BienLais { get; set; }
        public virtual LoaiSach LoaiSach { get; set; }
        public virtual NgonNgu NgonNgu { get; set; }
        public virtual NXB NXB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }
        public virtual TacGia TacGia { get; set; }
        public virtual ViTri ViTri { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongKe> ThongKes { get; set; }
    }
}
