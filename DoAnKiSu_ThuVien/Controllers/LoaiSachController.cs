using DoAnKiSu_ThuVien.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class LoaiSachController : Controller
    {
        DB_TVEntities objDoAnThuVienEntities = new DB_TVEntities();
        // GET: LoaiSach
        public ActionResult Index()
        {
            var lstLoaiSach = objDoAnThuVienEntities.LoaiSaches.ToList();

            return View(lstLoaiSach);
        }
    }
}