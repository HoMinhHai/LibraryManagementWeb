using DoAnKiSu_ThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class SystemController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        // GET: System
        public ActionResult Login()
        {
            ViewBag.Error = TempData["Error"];
            return View();
        }
        [HttpPost]
        public JsonResult GetListFunction()
        {
            if (Session["UserID"] == null)
                return Json(new { caution = "Phiên đăng nhập hết hạn" });
            int id = (int) Session["UserID"];
            var maNhom = db.TaiKhoanNoiBoes
                    .Where(t => t.ID_NV == id)
                    .Select(t => t.MaNhom)
                    .FirstOrDefault();
            var functions = (from pq in db.PhanQuyens
                             join cn in db.ChucNangCons on pq.ID_ChucNang equals cn.ID_ChucNang
                             join ph in db.PhanHes on cn.ID_PhanHe equals ph.ID_PhanHe
                             where pq.MaNhomND == maNhom && pq.CoQuyen == true
                             select new
                             {
                                 ph.TenPhanHe,
                                 cn.TenChucNang
                             }).ToList();
            var result = functions.GroupBy(f => f.TenPhanHe)
                                      .Select(g => new PhanHe_ChucNang
                                      {
                                          ten = g.Key,
                                          chucNang = g.Select(f => f.TenChucNang).ToList()
                                      }).ToList();

            return Json(result);

        }
        [HttpPost]
        public JsonResult GetAllFunctionByGroup(string tennhom)
        {
            List<ChucNang_PhanQuyen> lstChucNang = (from pq in db.PhanQuyens
                                        join cn in db.ChucNangCons
                                        on pq.ID_ChucNang equals cn.ID_ChucNang
                                        join n in db.NhomNguoiDungs
                                        on pq.MaNhomND equals n.MaNhom
                                        where n.TenNhom ==  tennhom
                                        select new ChucNang_PhanQuyen
                                        {
                                            tenChucNang = cn.TenChucNang,
                                            coQuyen = (bool) pq.CoQuyen
                                        }).ToList();
            return Json(lstChucNang);
        }
        public ActionResult QuanTriHeThong()
        {
            if(Session["UserID"] == null)
                return RedirectToAction("Login");
            int id_nv = 0;
            if (Session["UserID"] != null)
            {
                id_nv = (int)Session["UserID"];
            }
            String MaNhom = db.TaiKhoanNoiBoes.Find(id_nv).MaNhom.ToString();
            List<PhanHe> lstPhanHe = db.PhanHes.ToList();
            List<ChucNangCon> lstChucNang = db.PhanQuyens.Where(p => p.MaNhomND == MaNhom && p.CoQuyen == true).Select(c => c.ChucNangCon).ToList();
            foreach (PhanHe phanHe in lstPhanHe)
            {
                foreach (ChucNangCon chucnang in lstChucNang)
                {
                    if (chucnang.ID_PhanHe == phanHe.ID_PhanHe)
                        phanHe.ChucNangCons.Add(chucnang);
                }
            }
            return View(lstPhanHe);
        }
        [HttpPost]
        public ActionResult ProcessLogin(string uname, string psw)
        {
            int id = 0;
            id = (from tk in db.TaiKhoanNoiBoes
                  join nv in db.NhanViens
                  on tk.ID_NV equals nv.ID_NV
                  where nv.MaNhanVien == uname && tk.MatKhau == psw
                  select nv.ID_NV).FirstOrDefault();

            if (id != 0)
            {
                Session["UserID"] = id;
                return RedirectToAction("QuanTriHeThong");
            }
            else
            {
                TempData["Error"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("UserID");
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}