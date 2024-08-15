using DoAnKiSu_ThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class PhanQuyenController : Controller
    {
        // GET: PhanQuyen
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        public ActionResult GroupManagement()
        {
            List<NhomNguoiDung> lst = db.NhomNguoiDungs.ToList();
            return View(lst);
        }
        [HttpPost]
        public JsonResult getAllGroupUser()
        {
            List<string> lst = db.NhomNguoiDungs.Select(a => a.TenNhom).ToList();
            return Json(new {lst = lst});
        }
        [HttpPost]
        public JsonResult getEmployeeHaveNotAccount()
        {
            var employeesWithoutAccount = db.NhanViens
            .Where(e => !db.TaiKhoanNoiBoes.Any(a => a.ID_NV == e.ID_NV))
            .Select(e => new
            {
                EmployeeId = e.MaNhanVien,
                EmployeeName = e.HoTen
            })
            .ToList();

            return Json(employeesWithoutAccount);
        }
        [HttpPost]
        public JsonResult SaveRoleByGroup(string tenNhom, Role[] ds)
        {
            string maNhom = db.NhomNguoiDungs.Where(a => a.TenNhom == tenNhom).FirstOrDefault().MaNhom;
            
            for(int i = 0;  i < ds.Length; i++) {
                string name = ds[i].tenChucNang;
                int idChucNang = db.ChucNangCons.Where(a => a.TenChucNang == name).FirstOrDefault().ID_ChucNang;
                PhanQuyen item = db.PhanQuyens.Find(maNhom, idChucNang);
                if (ds[i].coQuyen == "True")
                    item.CoQuyen = true;
                else
                    item.CoQuyen = false;
            }
            db.SaveChanges();
            return Json(new { message = "Thành công" });
        }
    }
}