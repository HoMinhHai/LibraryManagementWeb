using DoAnKiSu_ThuVien.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class NhaXuatBanController : Controller
    {
        DB_TVEntities objDoAnThuVienEntities = new DB_TVEntities();
        // GET: NhaXuatBan
        public ActionResult Index()
        {
            var lstNhaXuatBan = objDoAnThuVienEntities.NXBs.ToList();

            return View(lstNhaXuatBan);
        }
    }
}