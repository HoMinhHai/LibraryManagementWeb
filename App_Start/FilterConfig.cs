﻿using System.Web;
using System.Web.Mvc;

namespace DoAnKiSu_ThuVien
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
