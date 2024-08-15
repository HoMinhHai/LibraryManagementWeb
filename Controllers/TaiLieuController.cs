using DoAnKiSu_ThuVien.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class TaiLieuController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        // GET: TaiLieu
        [HttpPost]
        public JsonResult GetInfoDocumentSearching(string SearchString, string category)
        {
            if (category == "Tìm theo tiêu đề")
            {
                var keywords = SearchString.Split(' ');
                var query = from tl in db.TaiLieux
                            join pb in db.PhienBanTaiLieux
                            on tl.ID_TL equals pb.ID_TaiLieu
                            where keywords.All(keyword => tl.TenTaiLieu.Contains(keyword))
                            select new ThongTinTaiLieuTimKiem
                            {
                                id = tl.ID_TL,
                                tenTL = tl.TenTaiLieu,
                                namXuatBan = pb.NamXuatBan,
                                anhBia = pb.AnhBia,
                                chuDe = tl.ChuDe1.TenChuDe,
                                loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                            };
                var result = query.ToList();
                return Json(result);
            }
            else if (category == "Chủ đề")
            {
                var keywords = SearchString.Split(' ');
                var query = from tl in db.TaiLieux
                            join cd in db.ChuDes
                            on tl.ChuDe equals cd.ID_ChuDe
                            join pb in db.PhienBanTaiLieux
                            on tl.ID_TL equals pb.ID_TaiLieu
                            where keywords.All(keyword => cd.TenChuDe.Contains(keyword))
                            select new ThongTinTaiLieuTimKiem
                            {
                                id = tl.ID_TL,
                                tenTL = tl.TenTaiLieu,
                                namXuatBan = pb.NamXuatBan,
                                anhBia = pb.AnhBia,
                                chuDe = tl.ChuDe1.TenChuDe,
                                loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                            };
                var result = query.ToList();
                return Json(result);
            }
            else if (category == "Tác giả")
            {
                var keywords = SearchString.Split(' ');

                var query = from tl in db.TaiLieux
                            join pb in db.PhienBanTaiLieux
                            on tl.ID_TL equals pb.ID_TaiLieu
                            where keywords.All(keyword => tl.TacGias.Any(tg => tg.TenTG.Contains(keyword)))
                            select new ThongTinTaiLieuTimKiem
                            {
                                id = tl.ID_TL,
                                tenTL = tl.TenTaiLieu,
                                namXuatBan = pb.NamXuatBan,
                                anhBia = pb.AnhBia,
                                chuDe = tl.ChuDe1.TenChuDe,
                                loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                            };

                var result = query.ToList();
                return Json(result);
            }
            else if (category == "Năm xuất bản")
            {
                int namXuatBan;
                if (int.TryParse(SearchString.Trim(), out namXuatBan))
                {
                    var query = from tl in db.TaiLieux
                                join pb in db.PhienBanTaiLieux
                                on tl.ID_TL equals pb.ID_TaiLieu
                                where pb.NamXuatBan == namXuatBan
                                select new ThongTinTaiLieuTimKiem
                                {
                                    id = tl.ID_TL,
                                    tenTL = tl.TenTaiLieu,
                                    namXuatBan = pb.NamXuatBan,
                                    anhBia = pb.AnhBia,
                                    chuDe = tl.ChuDe1.TenChuDe,
                                    loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                    tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                                };
                    var result = query.ToList();
                    return Json(result);
                }
                else
                {

                    var query = from tl in db.TaiLieux
                                join pb in db.PhienBanTaiLieux
                                on tl.ID_TL equals pb.ID_TaiLieu
                                where pb.NamXuatBan == -1
                                select new ThongTinTaiLieuTimKiem
                                {
                                    id = tl.ID_TL,
                                    tenTL = tl.TenTaiLieu,
                                    namXuatBan = pb.NamXuatBan,
                                    anhBia = pb.AnhBia,
                                    chuDe = tl.ChuDe1.TenChuDe,
                                    loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                    tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                                };
                    var result = query.ToList();
                    return Json(result);
                }
            }
            else
            {
                var keywords = SearchString.Split(' ');
                var query = from tl in db.TaiLieux
                            join pb in db.PhienBanTaiLieux
                            on tl.ID_TL equals pb.ID_TaiLieu
                            join nxb in db.NhaXuatBans
                            on pb.ID_NhaXuatBan equals nxb.ID_NXB
                            where keywords.All(keyword => nxb.TenNXB.Contains(keyword))
                            select new ThongTinTaiLieuTimKiem
                            {
                                id = tl.ID_TL,
                                tenTL = tl.TenTaiLieu,
                                namXuatBan = pb.NamXuatBan,
                                anhBia = pb.AnhBia,
                                chuDe = tl.ChuDe1.TenChuDe,
                                loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                            };
                var result = query.ToList();
                return Json(result);
            }
        }   
        public ActionResult Search(string SearchString, string category)
        {
            if(category == "Tiêu đề")
            {
                var keywords = SearchString.Split(' ');
                var query = from tl in db.TaiLieux
                            join pb in db.PhienBanTaiLieux
                            on tl.ID_TL equals pb.ID_TaiLieu
                            where keywords.All(keyword => tl.TenTaiLieu.Contains(keyword))
                            select new ThongTinTaiLieuTimKiem
                            {
                                id = tl.ID_TL,
                                tenTL = tl.TenTaiLieu,
                                namXuatBan = pb.NamXuatBan,
                                chuDe = tl.ChuDe1.TenChuDe,
                                loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                            };
                var result = query.ToList();
                return View(result);
            }   
            else if(category == "Chủ đề")
            {
                var keywords = SearchString.Split(' ');
                var query = from tl in db.TaiLieux
                            join cd in db.ChuDes
                            on tl.ChuDe equals cd.ID_ChuDe
                            join pb in db.PhienBanTaiLieux
                            on tl.ID_TL equals pb.ID_TaiLieu
                            where keywords.All(keyword => cd.TenChuDe.Contains(keyword))
                            select new ThongTinTaiLieuTimKiem
                            {
                                id = tl.ID_TL,
                                tenTL = tl.TenTaiLieu,
                                namXuatBan = pb.NamXuatBan,
                                chuDe = tl.ChuDe1.TenChuDe,
                                loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                            };
                var result = query.ToList();
                return View(result);
            }
            else if(category == "Tác giả")
            {
                var keywords = SearchString.Split(' ');

                var query = from tl in db.TaiLieux
                            join pb in db.PhienBanTaiLieux
                            on tl.ID_TL equals pb.ID_TaiLieu
                            where keywords.All(keyword => tl.TacGias.Any(tg => tg.TenTG.Contains(keyword)))
                            select new ThongTinTaiLieuTimKiem
                            {
                                id = tl.ID_TL,
                                tenTL = tl.TenTaiLieu,
                                namXuatBan = pb.NamXuatBan,
                                chuDe = tl.ChuDe1.TenChuDe,
                                loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                            };

                var result = query.ToList();
                return View(result);

            }
            else if(category == "Năm xuất bản")
            {
                int namXuatBan;
                if (int.TryParse(SearchString.Trim(), out namXuatBan))
                {
                    var query = from tl in db.TaiLieux
                                join pb in db.PhienBanTaiLieux
                                on tl.ID_TL equals pb.ID_TaiLieu
                                where pb.NamXuatBan == namXuatBan
                                select new ThongTinTaiLieuTimKiem
                                {
                                    id = tl.ID_TL,
                                    tenTL = tl.TenTaiLieu,
                                    namXuatBan = pb.NamXuatBan,
                                    chuDe = tl.ChuDe1.TenChuDe,
                                    loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                    tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                                };
                    var result = query.ToList();
                    return View(result);
                }
                else
                {

                    var query = from tl in db.TaiLieux
                                join pb in db.PhienBanTaiLieux
                                on tl.ID_TL equals pb.ID_TaiLieu
                                where pb.NamXuatBan == -1
                                select new ThongTinTaiLieuTimKiem
                                {
                                    id = tl.ID_TL,
                                    tenTL = tl.TenTaiLieu,
                                    namXuatBan = pb.NamXuatBan,
                                    chuDe = tl.ChuDe1.TenChuDe,
                                    loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                    tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                                };
                    var result = query.ToList();
                    return View(result);
                }
            }
            else
            {
                var keywords = SearchString.Split(' ');
                var query = from tl in db.TaiLieux
                            join pb in db.PhienBanTaiLieux
                            on tl.ID_TL equals pb.ID_TaiLieu
                            join nxb in db.NhaXuatBans
                            on pb.ID_NhaXuatBan equals nxb.ID_NXB
                            where keywords.All(keyword => nxb.TenNXB.Contains(keyword))
                            select new ThongTinTaiLieuTimKiem
                            {
                                id = tl.ID_TL,
                                tenTL = tl.TenTaiLieu,
                                namXuatBan = pb.NamXuatBan,
                                chuDe = tl.ChuDe1.TenChuDe,
                                loaiTL = tl.LoaiTaiLieu1.TenLoai,
                                tacGia = tl.TacGias.OrderBy(tg => tg.ID_TG).Select(tg => tg.TenTG).FirstOrDefault()
                            };
                var result = query.ToList();
                return View(result);
            }
       
           
           
        }
        [HttpPost]
        public JsonResult GetDocumentInfoByAndroid(int id)
        {
            var query = (from tl in db.TaiLieux
                         join pb in db.PhienBanTaiLieux
                         on tl.ID_TL equals pb.ID_TaiLieu
                         where tl.ID_TL == id
                         select new 
                         {
                             tenTL = tl.TenTaiLieu,
                             namXuatBan = pb.NamXuatBan,
                             tenNXB = pb.NhaXuatBan.TenNXB,
                             TacGia = tl.TacGias.Select(tg => tg.TenTG).FirstOrDefault(),
                             loaiTL = tl.LoaiTaiLieu1.TenLoai,
                             chuDe = tl.ChuDe1.TenChuDe,
                             anhBia = pb.AnhBia,
                       
                         }).FirstOrDefault();
            return Json(query);
        }
        [HttpPost]
        public JsonResult GetListCopyBookByAndroid(int id)
        {
            var query = (from tl in db.TaiLieux
                         where tl.ID_TL == id
                         join pb in db.PhienBanTaiLieux
                         on tl.ID_TL equals pb.ID_TaiLieu
                         join bs in db.BanSaoTaiLieux
                         on pb.ID_PhienBan equals bs.ID_PhienBan
                         select new
                         {
                             maTang = bs.KeSach.Tang1.ID_Tang,
                             maKe = bs.KeSach.MaKe,
                             maBS = bs.MaDangKyCaBiet,
                             trangThai = bs.TinhTrang
                         }).ToList();
            return Json(query);
        }
        public ActionResult Detail(int id)
        {
            var query = (from tl in db.TaiLieux
                         join pb in db.PhienBanTaiLieux
                         on tl.ID_TL equals pb.ID_TaiLieu
                         where tl.ID_TL == id
                         select new ThongTinTaiLieuTimKiem
                         {
                             tenTL = tl.TenTaiLieu,
                             namXuatBan = pb.NamXuatBan,
                             tenNXB = pb.NhaXuatBan.TenNXB,
                             dsTacGia = tl.TacGias.Select(tg => tg.TenTG).ToList(),
                             loaiTL = tl.LoaiTaiLieu1.TenLoai,
                             soTrang = pb.SoTrang,
                             chieuCao = pb.ChieuCao,
                             chuDe = tl.ChuDe1.TenChuDe,           
                             kyHieuPhanLoai = tl.KyHieuPhanLoai,
                             anhBia = pb.AnhBia,
                             dsBanSao = pb.BanSaoTaiLieux.ToList(),
                         }).FirstOrDefault();
            return View(query);
        }
        [HttpGet]
        public JsonResult GetDocumentByCategory(string type)
        {
            if(type == "research")
            {
                var result2 = (from tl in db.TaiLieux
                              join pb in db.PhienBanTaiLieux
                              on tl.ID_TL equals pb.ID_TaiLieu
                              join l in db.LoaiTaiLieux
                              on tl.LoaiTaiLieu equals l.ID_Loai
                              where l.TenLoai == "NCKH cấp bộ" || l.TenLoai == "NCKH cấp trường" || l.TenLoai == "NCKH cấp trường"
                              orderby tl.ID_TL descending
                              select new { tl.ID_TL, tl.TenTaiLieu, pb.AnhBia, tg = tl.TacGias.Select(tg => tg.TenTG).FirstOrDefault() }).ToList();
                if (result2 != null)
                    return Json(result2, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { error = "Không tìm thấy tài liệu" }, JsonRequestBehavior.AllowGet);
            }
            String condition = null;
            if (type == "book")
                condition = "Sách";
            else if (type == "speech")
                condition = "Khóa luận";
            else if (type == "thesis")
                condition = "Luận án";
            else 
                condition = "Luận văn";
            var result = (from tl in db.TaiLieux
                          join pb in db.PhienBanTaiLieux
                          on tl.ID_TL equals pb.ID_TaiLieu
                          join l in db.LoaiTaiLieux
                          on tl.LoaiTaiLieu equals l.ID_Loai
                          where l.TenLoai == condition
                          orderby tl.ID_TL descending
                          select new { tl.ID_TL, tl.TenTaiLieu, pb.AnhBia, tg = tl.TacGias.Select(tg => tg.TenTG).FirstOrDefault() }).ToList();
            if (result != null)
                return Json(result, JsonRequestBehavior.AllowGet);
            else
                return Json(new { error = "Không tìm thấy tài liệu" }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost] 
        public JsonResult LayDanhSachTaiLieuQuaHan(string maDocGia)
        {
            this.UpdateStatusBook(maDocGia);
            if (maDocGia == null)
                return Json(new { error = "Chưa nhập mã độc giả" });
            List<TaiLieuMuon> lstTaiLieu = new List<TaiLieuMuon>();
            List<ChiTietPhieuMuon> lstChiTiet = db.ChiTietPhieuMuons
               .Where(a => a.TrangThai == "Quá hạn")
               .Include(ctpm => ctpm.PhieuMuon)
               .Where(ctpm => ctpm.PhieuMuon.MaDocGia == maDocGia)
               .ToList();
            foreach (ChiTietPhieuMuon ct in lstChiTiet)
            {
                TaiLieuMuon tl = new TaiLieuMuon();
                tl.maDKCB = ct.MaTL;
                tl.tenTL = (from tls in db.TaiLieux
                            join pb in db.PhienBanTaiLieux
                            on tls.ID_TL equals pb.ID_TaiLieu
                            join bs in db.BanSaoTaiLieux
                            on pb.ID_PhienBan equals bs.ID_PhienBan
                            where bs.MaDangKyCaBiet == ct.MaTL
                            select tls.TenTaiLieu).FirstOrDefault();
                tl.ngayMuon = db.PhieuMuons.Find(ct.ID_PhieuMuon).NgayTao.ToString();
                tl.hanTra = ct.NgayTraSach.ToString();
                tl.trangThai = ct.TrangThai;
                lstTaiLieu.Add(tl);
            }
            return Json(lstTaiLieu);
        }
        [HttpPost]
        public JsonResult GetBookBorrowing(string maDocGia)
        {
            this.UpdateStatusBook(maDocGia);
            if (maDocGia == null)
                return Json(new { error = "Chưa nhập mã độc giả" });
            List<TaiLieuMuon> lstTaiLieu = new List<TaiLieuMuon>();
            List<ChiTietPhieuMuon> lstChiTiet = db.ChiTietPhieuMuons
               .Where(a => a.TrangThai == "Đang cho mượn")
               .Include(ctpm => ctpm.PhieuMuon)
               .Where(ctpm => ctpm.PhieuMuon.MaDocGia == maDocGia)
               .ToList();
            foreach (ChiTietPhieuMuon ct in lstChiTiet)
            {
                TaiLieuMuon tl = new TaiLieuMuon();
                tl.maDKCB = ct.MaTL;
                tl.tenTL = (from tls in db.TaiLieux
                            join pb in db.PhienBanTaiLieux
                            on tls.ID_TL equals pb.ID_TaiLieu
                            join bs in db.BanSaoTaiLieux
                            on pb.ID_PhienBan equals bs.ID_PhienBan
                            where bs.MaDangKyCaBiet == ct.MaTL
                            select tls.TenTaiLieu).FirstOrDefault();
                tl.ngayMuon = db.PhieuMuons.Find(ct.ID_PhieuMuon).NgayTao.ToString();
                tl.hanTra = ct.NgayTraSach.ToString();
                tl.trangThai = ct.TrangThai;
                lstTaiLieu.Add(tl);
            }
            return Json(lstTaiLieu);
        }
        [HttpPost]
        public JsonResult GetListBookBorrow(string maDocGia)
        {
            this.UpdateStatusBook(maDocGia);
            if (maDocGia == null)
                return Json(new { error = "Chưa nhập mã độc giả" });
            List<TaiLieuMuon> lstTaiLieu = new List<TaiLieuMuon>();
            List<ChiTietPhieuMuon> lstChiTiet = db.ChiTietPhieuMuons
               .Where(a => a.TrangThai == "Đang cho mượn" || a.TrangThai == "Quá hạn")
               .Include(ctpm => ctpm.PhieuMuon)
               .Where(ctpm => ctpm.PhieuMuon.MaDocGia == maDocGia)
               .ToList();
            if(lstChiTiet.Count < 1)
                return Json(new { status = "Độc giả không có nợ sách" });
            foreach (ChiTietPhieuMuon ct in lstChiTiet)
            {
                TaiLieuMuon tl = new TaiLieuMuon();
                tl.maDKCB = ct.MaTL;
                tl.tenTL = (from tls in db.TaiLieux
                            join pb in db.PhienBanTaiLieux
                            on tls.ID_TL equals pb.ID_TaiLieu
                            join bs in db.BanSaoTaiLieux
                            on pb.ID_PhienBan equals bs.ID_PhienBan
                            where bs.MaDangKyCaBiet == ct.MaTL
                            select tls.TenTaiLieu).FirstOrDefault();
                tl.ngayMuon = db.PhieuMuons.Find(ct.ID_PhieuMuon).NgayTao.ToString();
                tl.hanTra = ct.NgayTraSach.ToString();
                tl.trangThai = ct.TrangThai;
                lstTaiLieu.Add(tl);
            }
            return Json(new {ds = lstTaiLieu});
        }
        
        [HttpPost]
        public JsonResult ReturnDocument(string maTaiLieu)
        {
            if (Session["ReaderID"] == null)
                return Json(new { error = "Không tìm thấy độc giả" });
            if (maTaiLieu == null)
                return Json(new { error = "Chưa nhập mã tài liệu" });
            string maDocGia = Session["ReaderID"].ToString();
            ChiTietPhieuMuon ChiTiet = db.ChiTietPhieuMuons
                .Where(a => (a.TrangThai == "Đang cho mượn" || a.TrangThai == "Quá hạn") && a.MaTL == maTaiLieu)
                .Include(ctpm => ctpm.PhieuMuon)
                .Where(ctpm => ctpm.PhieuMuon.MaDocGia == maDocGia).FirstOrDefault();
            if (ChiTiet == null)
                return Json(new { error = "Tài liệu trả không hợp lệ" });
            else
            {
                ChiTiet.TrangThai = "Đã trả";
                ChiTiet.NgayTraSach = DateTime.Now;
                BanSaoTaiLieu bs = db.BanSaoTaiLieux.Find(ChiTiet.MaTL);
                bs.TinhTrang = "Sẵn có";
                db.SaveChanges();
                return Json(new { status = "Thành công", maDocGia = maDocGia });
            }  
        }
        
        public void UpdateStatusBook(string maDocGia)
        {
            if (maDocGia == null)
                return;
            List<ChiTietPhieuMuon> lstChiTiet = db.ChiTietPhieuMuons
                .Where(a => a.TrangThai == "Đang cho mượn")
                .Include(ctpm => ctpm.PhieuMuon)
                .Where(ctpm => ctpm.PhieuMuon.MaDocGia == maDocGia)
                .ToList();
            foreach (ChiTietPhieuMuon ct in lstChiTiet)
            {
                DateTime current = DateTime.Now;
                DateTime? hantra = ct.NgayTraSach;
                if (hantra < current)
                    ct.TrangThai = "Quá hạn";
            }
            db.SaveChanges();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TaiLieu p)
        {
            db.TaiLieux.Add(p);
            db.SaveChanges();
            return View();
        }





        public ActionResult Edit(int id)
        {
            //var sachModel = objDoAnThuVienEntities.Saches.Where(n => n.id_sach == id).FirstOrDefault();

            //if (sachModel == null)
            //{
            //    return HttpNotFound();
            //}

            //ViewBag.id_tac_gia = new SelectList(objDoAnThuVienEntities.TacGias, "id_tacgia", "ten_tacgia", sachModel.id_tac_gia);
            //ViewBag.id_loai_sach = new SelectList(objDoAnThuVienEntities.LoaiSaches, "id_loai_sach", "loai_sach", sachModel.id_loai_sach);
            //ViewBag.id_ngon_ngu = new SelectList(objDoAnThuVienEntities.NgonNgus, "id_ngon_ngu", "ten_ngon_ngu", sachModel.id_ngon_ngu);
            //ViewBag.id_vi_tri = new SelectList(objDoAnThuVienEntities.ViTris, "id_vi_tri", "ten_vi_tri", sachModel.id_vi_tri);

            //return View(sachModel);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(TaiLieu model)
        {
            //if (ModelState.IsValid)
            //{
            //    var EditModel = objDoAnThuVienEntities.Saches.Find(model.id_sach);

            //    if (EditModel != null)
            //    {
            //        EditModel.ten_sach = model.ten_sach;
            //        EditModel.id_tac_gia = model.id_tac_gia;
            //        EditModel.id_loai_sach = model.id_loai_sach;
            //        EditModel.id_ngon_ngu = model.id_ngon_ngu;
            //        EditModel.id_vi_tri = model.id_vi_tri;
            //        EditModel.ngay_nhap = model.ngay_nhap;
            //        EditModel.tom_tat = model.tom_tat;
            //        EditModel.trang_thai = model.trang_thai;
            //        EditModel.so_luong_con = model.so_luong_con;
            //        EditModel.price = model.price;

            //        objDoAnThuVienEntities.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "Sách không tìm thấy.");
            //    }
            //}

            //ViewBag.id_tac_gia = new SelectList(objDoAnThuVienEntities.TacGias, "id_tac_gia", "ten_tac_gia", model.id_tac_gia);
            //ViewBag.id_loai_sach = new SelectList(objDoAnThuVienEntities.LoaiSaches, "id_loai_sach", "loai_sach", model.id_loai_sach);
            //ViewBag.id_ngon_ngu = new SelectList(objDoAnThuVienEntities.NgonNgus, "id_ngon_ngu", "ten_ngon_ngu", model.id_ngon_ngu);
            //ViewBag.id_vi_tri = new SelectList(objDoAnThuVienEntities.ViTris, "id_vi_tri", "ten_vi_tri", model.id_vi_tri);

            //return View(model);
            return View();
        }
        public ActionResult Delete(int Id)
        {
            //DB_TVEntities db = new DB_TVEntities();
            //var doituongXoa = db.Saches.Find(Id);
            //db.Saches.Remove(doituongXoa);
            //db.SaveChanges();

            //return RedirectToAction("Index");
            return View();
            
        }
    }
}