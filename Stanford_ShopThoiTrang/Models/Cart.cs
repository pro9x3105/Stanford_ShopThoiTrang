using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopThoiTrang.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string TenSP { get; set; }
        public string MoTa { get; set; }
        public string Anh { get; set; }
        public double Gia { get; set; }
        public int SoLuong { get; set; }
    }
}