using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnKiSu_ThuVien.Context;

namespace DoAnKiSu_ThuVien.Models
{
    public class HomeModel
    {
        public List<Sach> ListProduct { get; set; }
        public List<LoaiSach> ListCategory { get; set; }
    }
}   