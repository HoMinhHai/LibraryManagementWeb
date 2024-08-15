using DoAnKiSu_ThuVien.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DoAnKiSu_ThuVien.Models;
using DoAnKiSu_ThuVien.Context;


namespace DoAnKiSu_ThuVien.Controllers
{
    public class HomeController : Controller
    {
        DB_TVEntities objDoAnThuVienEntities = new DB_TVEntities();

        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objDoAnThuVienEntities.LoaiSaches.ToList();
            objHomeModel.ListProduct = objDoAnThuVienEntities.Saches.ToList();
            return View(objHomeModel);
        }
        public ActionResult Index1()
        {
            var lstProducts = objDoAnThuVienEntities.Saches.ToList();

            return View(lstProducts);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Register( _user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var check = objDoAnThuVienEntities.Users.FirstOrDefault(s => s.email == _user.email);
        //        if (check == null)
        //        {
        //            _user.password = GetMD5(_user.password);
        //            objDoAnThuVienEntities.Configuration.ValidateOnSaveEnabled = false;
        //            objDoAnThuVienEntities.Users.Add(_user);
        //            objDoAnThuVienEntities.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.error = "Email already exists";
        //            return View();
        //        }


        //    }
        //    return View();


        //}

        //Create string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] formData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(formData);
            string byte2String = null;
            for (int i= 0;i<targetData.Length;i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Login(string email, string password)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var f_password = GetMD5(password);
        //        var data = objDoAnThuVienEntities.Users.Where(s => s.email.Equals(email) && s.password.Equals(f_password)).ToList();
        //        if (data.Count() > 0)
        //        {
        //            //add session
        //            Session["FullName"] = data.FirstOrDefault().user_first_name + " " + data.FirstOrDefault().user_last_name;
        //            Session["Email"] = data.FirstOrDefault().email;
        //            Session["idUser"] = data.FirstOrDefault().user_id;
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.error = "Login failed";
        //            return RedirectToAction("Login");
        //        }
        //    }
        //    return View();
        //}


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }
}