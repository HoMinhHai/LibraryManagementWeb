using DoAnKiSu_ThuVien.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class TacGiaController : Controller
    {

        DB_TVEntities objDoAnThuVienEntities = new DB_TVEntities();
        // GET: TacGia
        public ActionResult Index()
        {
            var lstTacGia = objDoAnThuVienEntities.TacGias.ToList();

            return View(lstTacGia);
        }
    }
}