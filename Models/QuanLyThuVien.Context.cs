﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuanLyThuVienEntities : DbContext
    {
        public QuanLyThuVienEntities()
            : base("name=QuanLyThuVienEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BanSaoTaiLieu> BanSaoTaiLieux { get; set; }
        public virtual DbSet<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }
        public virtual DbSet<ChucNangCon> ChucNangCons { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<DangKyDocGia> DangKyDocGias { get; set; }
        public virtual DbSet<DocGia> DocGias { get; set; }
        public virtual DbSet<DoiTuongDocGia> DoiTuongDocGias { get; set; }
        public virtual DbSet<KeSach> KeSaches { get; set; }
        public virtual DbSet<LoaiTaiLieu> LoaiTaiLieux { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public virtual DbSet<NhomNguoiDung> NhomNguoiDungs { get; set; }
        public virtual DbSet<PhanHe> PhanHes { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<PhienBanTaiLieu> PhienBanTaiLieux { get; set; }
        public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<TaiKhoanNoiBo> TaiKhoanNoiBoes { get; set; }
        public virtual DbSet<TaiLieu> TaiLieux { get; set; }
        public virtual DbSet<Tang> Tangs { get; set; }
        public virtual DbSet<TheThuVien> TheThuViens { get; set; }
    }
}
