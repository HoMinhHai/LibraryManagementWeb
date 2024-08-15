using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnKiSu_ThuVien.Context;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class ProductController : Controller
    {
        DB_TVEntities objDoAnThuVienEntities = new DB_TVEntities();
        // GET: Product

      
        public ActionResult Detail(int Id)
        {
            var objProduct = objDoAnThuVienEntities.Saches.Where(n=>n.id_sach ==Id ).FirstOrDefault();
            return View(objProduct);
        }

        public ActionResult Index()
        {
            var lstProducts = objDoAnThuVienEntities.Saches.ToList();

            return View(lstProducts);
        }
        //public ActionResult Order()
        //{
        //    var lstOrder = objDoAnThuVienEntities.Orders.ToList();

        //    return View(lstOrder);
        //}

        public ActionResult Create()
        {
            ViewBag.id_tac_gia = new SelectList(objDoAnThuVienEntities.TacGias, "id_tac_gia", "ten_tac_gia");
            ViewBag.id_loai_sach = new SelectList(objDoAnThuVienEntities.LoaiSaches, "id_loai_sach", "ten_loai_sach");
            ViewBag.id_ngon_ngu = new SelectList(objDoAnThuVienEntities.LoaiSaches, "id_ngon_ngu", "ten_ngon_ngu");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Sach model)
        {
            DB_TVEntities db = new DB_TVEntities();
            //Lưu dữ liệu vào db
            if (string.IsNullOrEmpty(model.ten_sach) == true)
            {
                ModelState.AddModelError("", "Tên sản phẩm không được để trống!!!");
                return View(model);
            }
            if (model.price <= 0)
            {
                ModelState.AddModelError("", "Giá bán phải lớn hơn 0!!!");
                return View(model);
            }
            model.id_tac_gia = int.Parse(Request["id_tac_gia"]);
            //Lưu
            db.Saches.Add(model);
            db.SaveChanges();
            if (model.id_sach > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Lỗi không lưu được vào DB!!!");
                return View(model);
            }
        }





        public ActionResult Edit(int Id)
        {
            var productModel = objDoAnThuVienEntities.Saches.Where(n => n.id_sach == Id).FirstOrDefault();
            return View(productModel);
        }
        [HttpPost]
        public ActionResult Edit(Sach model)
        {
            //Lưu dữ liệu vào db
            if (string.IsNullOrEmpty(model.ten_sach) == true)
            {
                ModelState.AddModelError("", "Tên sản phẩm không được để trống!!!");
                return View(model);
            }
            if (model.price <= 0)
            {
                ModelState.AddModelError("", "Giá bán phải lớn hơn 0!!!");
                return View(model);
            }
            //Tìm đối tượng cần sửa
            var EditModel = objDoAnThuVienEntities.Saches.Where(n => n.id_sach == model.id_sach).FirstOrDefault();
            //Gắn giá trị mới cho đối tượng
            EditModel.ten_sach = model.ten_sach;
            EditModel.avatar = model.avatar;
            EditModel.LoaiSach.id_loai_sach = model.LoaiSach.id_loai_sach ;
            EditModel.tom_tat = model.tom_tat;
          
            EditModel.price = model.price;
           
            //EditModel.type_id = model.type_id;
           
            //EditModel.Brand.brand_id = model.Brand.brand_id;
            var id = objDoAnThuVienEntities.SaveChanges();
            if (id > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Lỗi không lưu được vào DB!!!");
                return View(model);
            }
        }

        public ActionResult Delete(int Id)
        {
            var productModel = objDoAnThuVienEntities.Saches.Where(n => n.id_sach== Id).FirstOrDefault();
            objDoAnThuVienEntities.Saches.Remove(productModel);
            objDoAnThuVienEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}