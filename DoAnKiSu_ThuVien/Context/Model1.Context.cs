﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_TVEntities : DbContext
    {
        public DB_TVEntities()
            : base("name=DB_TVEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BienLai> BienLais { get; set; }
        public virtual DbSet<DocGia> DocGias { get; set; }
        public virtual DbSet<KhoaHoc> KhoaHocs { get; set; }
        public virtual DbSet<LoaiSach> LoaiSaches { get; set; }
        public virtual DbSet<LyDo> LyDoes { get; set; }
        public virtual DbSet<NgonNgu> NgonNgus { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NXB> NXBs { get; set; }
        public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<ThongKe> ThongKes { get; set; }
        public virtual DbSet<ViTri> ViTris { get; set; }
    }
}
