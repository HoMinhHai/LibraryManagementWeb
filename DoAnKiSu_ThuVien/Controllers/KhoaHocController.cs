using DoAnKiSu_ThuVien.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class KhoaHocController : Controller
    {
        DB_TVEntities objDoAnThuVienEntities = new DB_TVEntities();
        // GET: KhoaHoc
        public ActionResult Index()
        {
            var lstKhoaHoc = objDoAnThuVienEntities.KhoaHocs.ToList();

            return View(lstKhoaHoc);
        }
    }
}