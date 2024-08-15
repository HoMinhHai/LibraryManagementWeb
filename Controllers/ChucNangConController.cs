using DoAnKiSu_ThuVien.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
namespace DoAnKiSu_ThuVien.Controllers
{
    public class ChucNangConController : Controller
    {
        // GET: ChucNangCon
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        public ActionResult QuanLyNguoiDung()
        {
            var query = from tk in db.TaiKhoanNoiBoes
                        join nh in db.NhomNguoiDungs
                        on tk.MaNhom equals nh.MaNhom
                        join nv in db.NhanViens
                        on tk.ID_NV equals nv.ID_NV
                        select new TaiKhoan_Nhom_NhanVien
                        {
                            tendn = nv.MaNhanVien,
                            tennhom = nh.TenNhom,
                            trangthai = tk.TrangThai,
                            tennv = nv.HoTen
                        };

            var result = query.ToList();

            return View(result);
        }
        public ActionResult QuanLyDocGia()
        {
            
            return View();
        }
        [HttpPost]
        public JsonResult GetInfoReader(string[] dsMaDocGia)
        {
            List<DocGia_TheThuVien> dglst = new List<DocGia_TheThuVien>();
            for(int i = 0; i< dsMaDocGia.Length; i++)
            {
                var dg = db.DocGias.Find(dsMaDocGia[i]);
                if(dg != null)
                {
                    DocGia_TheThuVien item = new DocGia_TheThuVien();
                    item.MaDocGia = dg.MaDocGia;
                    item.TenDocGia = dg.TenDocGia;
                    item.DonVi = dg.DonVi;
                    item.doiTuong = dg.DoiTuongDocGia.TenDoiTuong;
                    item.NgaySinh = dg.NgaySinh.ToString();
                    item.hinhanh = dg.TheThuVien.HinhThe;
                    dglst.Add(item);
                }
            }
            return (Json(dglst));
         
        }
        [HttpPost]
        public JsonResult SearchReader(string category, string input)
        {
            if (category == "Mã độc giả")
            {
                var DocGiaTimKiem = (from dg in db.DocGias
                                     join tv in db.TheThuViens
                             on dg.MaDocGia equals tv.MaDocGia
                                     join dt in db.DoiTuongDocGias
                                     on dg.ID_DoiTuong equals dt.ID_DoiTuong
                                     where dg.MaDocGia == input
                                     select new { dg.MaDocGia, dg.TenDocGia, dg.DonVi, dg.GioiTinh, dg.Email, dg.DienThoai, dt.TenDoiTuong, tv.TrangThai }).FirstOrDefault();
                return Json(DocGiaTimKiem);
            }
            else if (category == "Email")
            {
                var DocGiaTimKiem = (from dg in db.DocGias
                                     join tv in db.TheThuViens
                             on dg.MaDocGia equals tv.MaDocGia
                                     join dt in db.DoiTuongDocGias
                                     on dg.ID_DoiTuong equals dt.ID_DoiTuong
                                     where dg.Email == input
                                     select new { dg.MaDocGia, dg.TenDocGia, dg.DonVi, dg.GioiTinh, dg.Email, dg.DienThoai, dt.TenDoiTuong, tv.TrangThai }).FirstOrDefault();
                return Json(DocGiaTimKiem);
            }
            else if (category == "Điện thoại")
            {
                var DocGiaTimKiem = (from dg in db.DocGias
                                     join tv in db.TheThuViens
                             on dg.MaDocGia equals tv.MaDocGia
                                     join dt in db.DoiTuongDocGias
                                     on dg.ID_DoiTuong equals dt.ID_DoiTuong
                                     where dg.DienThoai == input
                                     select new { dg.MaDocGia, dg.TenDocGia, dg.DonVi, dg.GioiTinh, dg.Email, dg.DienThoai, dt.TenDoiTuong, tv.TrangThai }).FirstOrDefault();
                return Json(DocGiaTimKiem);
            }
            else if (category == "Họ tên")
            {
                var DocGiaTimKiem = (from dg in db.DocGias
                                     join tv in db.TheThuViens on dg.MaDocGia equals tv.MaDocGia
                                     join dt in db.DoiTuongDocGias on dg.ID_DoiTuong equals dt.ID_DoiTuong
                                     where dg.TenDocGia.ToLower().Contains(input.ToLower())
                                     select new { dg.MaDocGia, dg.TenDocGia, dg.DonVi, dg.GioiTinh, dg.Email, dg.DienThoai, dt.TenDoiTuong, tv.TrangThai }).ToList();
                return Json(DocGiaTimKiem);

            }
            else
            {
                var DocGiaTimKiem = (from dg in db.DocGias
                                     join tv in db.TheThuViens on dg.MaDocGia equals tv.MaDocGia
                                     join dt in db.DoiTuongDocGias on dg.ID_DoiTuong equals dt.ID_DoiTuong
                                     where dt.TenDoiTuong.ToLower().Contains(input.ToLower())
                                     select new { dg.MaDocGia, dg.TenDocGia, dg.DonVi, dg.GioiTinh, dg.Email, dg.DienThoai, dt.TenDoiTuong, tv.TrangThai }).ToList();
                return Json(DocGiaTimKiem);

            }

        }
        [HttpPost]
        public JsonResult GetReaderByType(string loaiDoiTuong)
        {
            var lstDocGia = (from dg in db.DocGias
                             join tv in db.TheThuViens
                             on dg.MaDocGia equals tv.MaDocGia
                             join dt in db.DoiTuongDocGias
                             on dg.ID_DoiTuong equals dt.ID_DoiTuong
                             where dt.TenDoiTuong == loaiDoiTuong
                             select new
                             {
                                 dg.MaDocGia,
                                 dg.TenDocGia,
                                 dg.DonVi,
                                 dg.DiaChi,
                                 dg.GioiTinh,
                                 dg.Email,
                                 dg.DienThoai,
                                 dt.TenDoiTuong,
                                 tv.TrangThai
                             }).ToList();
            if (lstDocGia.Count < 1)
                return Json(new { message = "Không có dữ liệu" });
            return Json(lstDocGia);
        }
        [HttpPost]
        public JsonResult ImportReaderByExcel(DocGia_TheThuVien[] ds)
        {
            if (ds.Count() < 1)
                return Json(new { error = "Dữ liệu rỗng" });
            
           foreach(var dg in ds)
           {
                if (dg.MaDocGia == null || dg.TenDocGia == null || dg.DonVi == null)
                    continue;
                DocGia RdNew = new DocGia();
                TheThuVien ttNew  = new TheThuVien();
                RdNew.MaDocGia = dg.MaDocGia;
                RdNew.ID_DoiTuong = 3;
                RdNew.TenDocGia = dg.TenDocGia;
                RdNew.DonVi = dg.DonVi;
                if(dg.NgaySinh != null)
                    RdNew.NgaySinh = Convert.ToDateTime(dg.NgaySinh.Substring(0, 10));
                RdNew.GioiTinh = dg.GioiTinh;
                RdNew.DiaChi = dg.DiaChi;
                RdNew.CCCD = dg.CCCD;
                RdNew.DienThoai = dg.DienThoai;
                RdNew.Email = dg.Email;
                RdNew.KhoaHoc = dg.KhoaHoc;
                ttNew.MaDocGia = dg.MaDocGia;
                ttNew.MatKhau = dg.MaDocGia;
                if (dg.NgayTao != null)
                    ttNew.NgayTao = Convert.ToDateTime(dg.NgayTao.Substring(0, 10));
                ttNew.NguoiThucHien = 1;
                if (dg.NgayTao != null)
                    ttNew.NgayHetHan = Convert.ToDateTime(dg.NgayTao.Substring(0, 10)).AddYears(1);
                ttNew.TrangThai = "Hoạt động";
                db.DocGias.Add(RdNew);
                db.TheThuViens.Add(ttNew);
           }
           db.SaveChanges();
            return Json(new { message = "Thành công" });
        }
        public ActionResult DownloadFileReaderSample()
        {
            string filePath = Server.MapPath("~/Content/Files/Nhap_khau_danh_sach_ban_doc_sinh_vien.xls");

            // Kiểm tra xem file có tồn tại không
            if (System.IO.File.Exists(filePath))
            {
                // Lấy tên file
                string fileName = Path.GetFileName(filePath);

                // Đọc nội dung của file
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Trả về file cho client để tải về
                return File(fileBytes, "application/octet-stream", fileName);
            }
            else
            {
                // Xử lý khi file không tồn tại (tuỳ chọn)
                return Content("File not found!");
            }
        }
        public ActionResult ImportReader()
        {
            return View();
        }
        public ActionResult ThemNguoiDung()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(string[] data)
        {
            String matkhau = data[0];
            String nhom = data[1];
            String trangThai = data[2];
            String nhanvien = data[3];
            int index = nhanvien.IndexOf(" - ");
            string manv = nhanvien.Substring(0, index);
            TaiKhoanNoiBo tk = new TaiKhoanNoiBo();
            tk.MatKhau = matkhau;
            tk.MaNhom = db.NhomNguoiDungs.Where(a => a.TenNhom == nhom).Select(a => a.MaNhom).FirstOrDefault();
            tk.TrangThai = trangThai;
            tk.ID_NV = (int)db.NhanViens.Where(a => a.MaNhanVien == manv).Select(a => a.ID_NV).FirstOrDefault();
            db.TaiKhoanNoiBoes.Add(tk);
            db.SaveChanges();
            return Json(new
            {
                errCode = 0,
                status = "Thành công"
            });

        }
        public ActionResult QuanLyMuonTra()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "System");
            return View();
        }
        [HttpPost]
        public JsonResult Search(string inputDoc)
        {
            System.Diagnostics.Debug.WriteLine(inputDoc);
            var result = db.DocGias.Find(inputDoc);
            if (result != null)
            {
                DateTime? ngayTao = result.TheThuVien.NgayTao;
                Session["ReaderID"] = result.MaDocGia;
                string ngayThangNam = ngayTao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                return Json(new
                {
                    TenDocGia = result.TenDocGia,
                    TrangThai = result.TheThuVien.TrangThai,
                    ChucVu = result.DoiTuongDocGia.TenDoiTuong,
                    SoDu = result.SoDu,
                    NgayCap = ngayThangNam,
                    DonVi = result.DonVi,
                    DienThoai = result.DienThoai,
                    DiaChi = result.DiaChi,
                    KhoaHoc = result.KhoaHoc,
                    Email = result.Email
                });
            }
            else
            {
                return Json(new { error = "Không tìm thấy độc giả" });
            }
        }
        [HttpPost]
        public JsonResult SaveBookBorrowNote(string[] dsTaiLieu)
        {
            if (Session["UserID"] == null)
                return Json(new
                {
                    message = "Thông tin nhân viên không hợp lệ",
                    errCode = 1
                });
            else if (Session["ReaderID"] == null)
                return Json(new
                {
                    message = "Chưa nhập thông tin độc giả",
                    errCode = 2
                });
            else if (dsTaiLieu.Length < 1)
                return Json(new
                {
                    message = "Chưa nhập tài liệu",
                    errCode = 3
                });

            int dk = this.CheckNumberBookAcceptBorrow(dsTaiLieu.Length);
            if (dk > 0)
            {
                return Json(new
                {
                    errCode = 4,
                    message = "Độc giả chỉ được phép mượn " + dk + " tài liệu"
                });
            }
            int code = 0;
            if (db.PhieuMuons.OrderByDescending(a => a.ID_PhieuMuon).FirstOrDefault() != null)
                code = (db.PhieuMuons.OrderByDescending(a => a.ID_PhieuMuon).FirstOrDefault().ID_PhieuMuon) + 1;
            else
                code = 1;
            PhieuMuon pm = new PhieuMuon();
            pm.NgayTao = DateTime.Now;
            pm.MaDocGia = Session["ReaderID"].ToString();
            pm.MaPhieuMuon = Session["ReaderID"].ToString() + "-" + code.ToString();
            pm.ID_NhanVien = (int)Session["UserID"];
            pm.TrangThai = "Tạo mới";
            db.PhieuMuons.Add(pm);
            db.SaveChanges();
            SaveDetailBookBorrow(dsTaiLieu);
            UpdateStatusBook(dsTaiLieu);
            return Json(new { message = "Thành công" });
        }
        public int CheckNumberBookAcceptBorrow(int soLuongMuon)
        {
            string readerId = Session["ReaderID"].ToString();
            int soLuongToiDa = (int)db.DocGias.Find(readerId).DoiTuongDocGia.SoLuongTaiLieu;

            int soLuongSachDangMuon = db.ChiTietPhieuMuons.Where(b => b.TrangThai == "Đang cho mượn" || b.TrangThai == "Quá hạn").Include(a => a.PhieuMuon).Where(a => a.PhieuMuon.MaDocGia == readerId).Count();
            if ((soLuongMuon + soLuongSachDangMuon) > soLuongToiDa)
            {
                return soLuongToiDa;
            }
            else
                return 0;
        }
        private void UpdateStatusBook(string[] lst)
        {
            for (int i = 0; i < lst.Length; i++)
            {
                BanSaoTaiLieu item = db.BanSaoTaiLieux.Find(lst[i]);
                item.TinhTrang = "Đang cho mượn";
            }
            db.SaveChanges();
        }
        private void SaveDetailBookBorrow(string[] lst)
        {
            int? soNgayMuon = db.DocGias.Find(Session["ReaderID"]).DoiTuongDocGia.SoNgay;
            PhieuMuon pm = db.PhieuMuons.OrderByDescending(a => a.ID_PhieuMuon).FirstOrDefault();
            for (int i = 0; i < lst.Length; i++)
            {
                ChiTietPhieuMuon ct = new ChiTietPhieuMuon();
                ct.MaChiTiet = pm.MaPhieuMuon + "-" + i;
                ct.ID_PhieuMuon = pm.ID_PhieuMuon;
                ct.MaTL = lst[i];
                ct.TrangThai = "Đang cho mượn";
                ct.NgayTraSach = pm.NgayHetHan = DateTime.Now.AddDays((double)soNgayMuon);
                db.ChiTietPhieuMuons.Add(ct);
            }
            db.SaveChanges();
        }

        public JsonResult SearchBook(string inputDoc)
        {
            System.Diagnostics.Debug.WriteLine(inputDoc);
            if (Session["ReaderID"] == null)
                return Json(new
                {
                    errCode = 3,
                    message = "Chưa nhập thông tin độc giả",
                });
            int? NumberDayAcceptBorrow = db.DocGias.Find(Session["ReaderID"]).DoiTuongDocGia.SoNgay;
            String expiredDay = DateTime.Now.AddDays((double)NumberDayAcceptBorrow).ToString("dd/MM/yyyy");
            System.Diagnostics.Debug.WriteLine("Hạn trả: " + expiredDay);
            System.Diagnostics.Debug.WriteLine(inputDoc);
            var result = (from bs in db.BanSaoTaiLieux
                          join pb in db.PhienBanTaiLieux
                          on bs.ID_PhienBan equals pb.ID_PhienBan
                          join tl in db.TaiLieux
                          on pb.ID_TaiLieu equals tl.ID_TL
                          where bs.MaDangKyCaBiet == inputDoc
                          select new
                          {
                              maDKCB = bs.MaDangKyCaBiet,
                              tenTL = tl.TenTaiLieu,
                              tinhTrang = bs.TinhTrang,
                              hanTra = expiredDay,
                          }).FirstOrDefault();
            if (result != null)
            {
                if (result.tinhTrang.Equals("Đang cho mượn"))
                    return Json(new
                    {
                        errCode = 1,
                        message = "Tài liệu đang được cho mượn",
                    });
                if (result.tinhTrang.Equals("Không lưu thông"))
                    return Json(new
                    {
                        errCode = 2,
                        message = "Tài liệu này ở trạng thái không lưu thông",
                    });
                return Json(new
                {
                    maDKCB = result.maDKCB,
                    tenTL = result.tenTL,
                    hanTra = result.hanTra,
                });
            }
            else
            {
                return Json(new { error = "Không tìm thấy tài liệu" });
            }
        }

        public ActionResult QuanLyTaiLieu()
        {

            var lstProducts = db.TaiLieux.ToList();

            return View(lstProducts);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TaiLieu p)
        {
            QuanLyThuVienEntities db = new QuanLyThuVienEntities();
            db.TaiLieux.Add(p);
            db.SaveChanges();
            return RedirectToAction("QuanLyTaiLieu");
        }


        public ActionResult Edit(int id)
        {
            var sachModel = db.TaiLieux.Where(n => n.ID_TL == id).FirstOrDefault();

            if (sachModel == null)
            {
                return HttpNotFound();
            }

            //ViewBag.id_ltl = new SelectList(db.LoaiTaiLieux, "id_Loai", "", sachModel);
            //ViewBag.id_cd = new SelectList(db.ChuDes, "id_ltl", "ten chu de", sachModel.);

            return View(sachModel);
        }
        [HttpPost]
        public ActionResult Edit(TaiLieu model)
        {
            if (ModelState.IsValid)
            {
                var EditModel = db.TaiLieux.Find(model.ID_TL);

                if (EditModel != null)
                {
                    EditModel.TenTaiLieu = model.TenTaiLieu;
                    EditModel.KyHieuPhanLoai = model.KyHieuPhanLoai;
                    EditModel.LoaiTaiLieu = model.LoaiTaiLieu;
                    EditModel.ChuDe = model.ChuDe;

                    db.SaveChanges();
                    return RedirectToAction("QuanLyTaiLieu");
                }
                else
                {
                    ModelState.AddModelError("", "Sách không tìm thấy.");
                }
            }
            //ViewBag.id_ltl = new SelectList(db.LoaiTaiLieux, "id_Loai", "", model.);
            //ViewBag.id_cd = new SelectList(db.ChuDes, "id_ltl", "ten chu de", sachModel.);

            return View(model);
        }
        public ActionResult QuanLyPhanQuyen()
        {
            return View();
        }
        public ActionResult Delete(int Id)
        {
            QuanLyThuVienEntities db = new QuanLyThuVienEntities();
            var doituongXoa = db.TaiLieux.Find(Id);
            db.TaiLieux.Remove(doituongXoa);
            db.SaveChanges();

            return RedirectToAction("QuanLyTaiLieu");
        }
        public ActionResult ExportToExcel()
        {
            var thongKeRecords = db.PhieuMuons // Tên của DbSet được tạo ra
                .Select(tk => new
                {
                    tk.MaPhieuMuon,
                    tk.NgayTao,
                    tk.NgayHetHan,
                    tk.MaDocGia,
                    tk.ID_NhanVien,
                    tk.TrangThai
                }).ToList();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ThongKe");
                var row = 1;

                // Thêm tiêu đề cột
                worksheet.Cells[row, 1].Value = "MaPhieuMuon";
                worksheet.Cells[row, 2].Value = "NgayTao";
                worksheet.Cells[row, 3].Value = "NgayHetHan";
                worksheet.Cells[row, 4].Value = "MaDocGia";
                worksheet.Cells[row, 5].Value = "ID Sách";
                worksheet.Cells[row, 6].Value = "TrangThai";

                // Thêm dữ liệu
                foreach (var record in thongKeRecords)
                {
                    row++;
                    worksheet.Cells[row, 1].Value = record.MaPhieuMuon;
                    worksheet.Cells[row, 2].Value = record.NgayTao.HasValue
                                 ? record.NgayTao.Value.ToString("yyyy-MM-dd")
                                 : string.Empty;
                    worksheet.Cells[row, 3].Value = record.NgayHetHan.HasValue
                                 ? record.NgayHetHan.Value.ToString("yyyy-MM-dd")
                                 : string.Empty;
                    worksheet.Cells[row, 4].Value = record.MaDocGia;
                 
                    worksheet.Cells[row, 5].Value = record.ID_NhanVien;
                    worksheet.Cells[row, 6].Value = record.TrangThai;
                }

                // Lưu file Excel
                var excelData = package.GetAsByteArray();
                var fileName = "ThongKe.xlsx";
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        [HttpPost]
        public JsonResult GetInfoReaderByCode(string code)
        {
            var dg = db.DocGias.Find(code);
            if (dg != null)
            {
                var result = new
                {
                    anh = dg.TheThuVien.HinhThe,
                    hoTen = dg.TenDocGia,
                    maDocGia = dg.MaDocGia,
                    donVi = dg.DonVi,
                    email = dg.Email,
                    gioiTinh = dg.GioiTinh,
                    tinhTrangThe = dg.TheThuVien.TrangThai
                };
                return Json(result);
            }
                
            return Json(null);
        }
        public ActionResult ExportToPdf()
        {


          
            List<TheThuVien> theThuViens = db.TheThuViens.ToList();
            // Đảm bảo NgayTao là ngày hiện tại và NgayHetHan là một năm sau NgayTao
            foreach (var theThuVien in theThuViens)
            {
                if (theThuVien.NgayTao != DateTime.Now.Date)
                {
                    theThuVien.NgayTao = DateTime.Now;
                }

                if (theThuVien.NgayHetHan != theThuVien.NgayTao.Value.Date.AddYears(1))
                {
                    theThuVien.NgayHetHan = theThuVien.NgayTao.Value.Date.AddYears(1);
                }

            }
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            document.Open();
            PdfPTable table = new PdfPTable(6); // 6 cột

            // Thêm tiêu đề
            table.AddCell("MaDocGia");
            table.AddCell("MatKhau");
            table.AddCell("NgayTao");
            table.AddCell("NguoiThucHien");
            table.AddCell("NgayHetHan");
            table.AddCell("TrangThai");

            // Thêm dữ liệu
            foreach (var theThuVien in theThuViens)
            {
                table.AddCell(theThuVien.MaDocGia);
                table.AddCell(theThuVien.MatKhau);
                table.AddCell(theThuVien.NgayTao.HasValue?theThuVien.NgayTao.Value.ToString("yyyy-MM-dd"):string.Empty);
                table.AddCell(theThuVien.NguoiThucHien.ToString());
                table.AddCell(theThuVien.NgayHetHan.HasValue ? theThuVien.NgayHetHan.Value.ToString("yyyy-MM-dd") : string.Empty);
                table.AddCell(theThuVien.TrangThai);
            }

            document.Add(table);
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return File(workStream, "application/pdf", "TheThuVien.pdf");
        }
        public ActionResult ThanhLyTaiLieu()
        {
            var lstProducts = db.TaiLieux
                    .Where(b => b.NgayNhap < new DateTime(2021, 1, 1))
                    .ToList();

            return View(lstProducts);
        }
       
    }
}