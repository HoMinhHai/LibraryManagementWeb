using DoAnKiSu_ThuVien.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class NgonNguController : Controller
    {
        DB_TVEntities objDoAnThuVienEntities = new DB_TVEntities();
        // GET: NgonNgu
        public ActionResult Index()
        {
            var lstNgonNgu = objDoAnThuVienEntities.NgonNgus.ToList();

            return View(lstNgonNgu);
        }
    }
}