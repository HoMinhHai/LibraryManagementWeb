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
    
    public partial class ChucNangCon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChucNangCon()
        {
            this.PhanQuyens = new HashSet<PhanQuyen>();
        }
    
        public int ID_ChucNang { get; set; }
        public Nullable<int> ID_PhanHe { get; set; }
        public string TenChucNang { get; set; }
    
        public virtual PhanHe PhanHe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }
    }
}
