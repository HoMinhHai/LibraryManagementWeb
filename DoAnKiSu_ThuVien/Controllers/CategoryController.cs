using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnKiSu_ThuVien.Context;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class CategoryController : Controller
    {
        DB_TVEntities objWebsiteBanHangEntities = new DB_TVEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objWebsiteBanHangEntities.LoaiSaches.ToList();

            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objWebsiteBanHangEntities.LoaiSaches.Where(n=>n.id_loai_sach == Id).ToList();
            return View(listProduct);
        }
    }
}