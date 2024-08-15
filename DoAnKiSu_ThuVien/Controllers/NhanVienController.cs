using DoAnKiSu_ThuVien.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: NhanVien
        DB_TVEntities objDoAnThuVienEntities = new DB_TVEntities();
        // GET: LoaiSach
        public ActionResult Index()
        {
            var lstNhanVien = objDoAnThuVienEntities.NhanViens.ToList();

            return View(lstNhanVien);
        }
    }
}