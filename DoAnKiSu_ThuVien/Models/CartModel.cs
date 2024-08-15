using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnKiSu_ThuVien.Context;

namespace DoAnKiSu_ThuVien.Models
{
    public class CartModel
    {
        public Sach Product { get; set; }
        public int Quantity { get; set; }
    }
}