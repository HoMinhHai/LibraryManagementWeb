using DoAnKiSu_ThuVien.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class LyDoController : Controller
    {
        DB_TVEntities objDoAnThuVienEntities = new DB_TVEntities();
        // GET: LyDo
        public ActionResult Index()
        {
            var lstLiDo = objDoAnThuVienEntities.LyDoes.ToList();

            return View(lstLiDo);
        }
    }
}