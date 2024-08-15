using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien.Controllers
{
    public class AuthorizeUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Kiểm tra xem người dùng có cookie đăng nhập hay không
            var cookie = filterContext.HttpContext.Request.Cookies["LoginCookie"];
            if (cookie == null || string.IsNullOrEmpty(cookie["Username"]))
            {
                // Chuyển hướng người dùng đến trang đăng nhập nếu chưa đăng nhập
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                    { "controller", "Home" },
                    { "action", "Index" }
                    });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}