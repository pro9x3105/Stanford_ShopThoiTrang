using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopThoiTrang.Models
{
    public class PhanQuyenViewModel
    {
        public int Id { get; set; }

        public int? ChucNangId { get; set; }

        public int? VaiTroId { get; set; }

        public bool? Xem { get; set; }

        public bool? Them { get; set; }

        public bool? Sua { get; set; }

        public bool? Xoa { get; set; }
    }
}