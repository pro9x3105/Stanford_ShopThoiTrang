using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopThoiTrang.Models
{
    public class ChuongTrinhChiTietViewModel
    {
        public int Id { get; set; }

        public int? ChuongTrinhId { get; set; }

        public int? SanPhamId { get; set; }

        public double? GiamGia { get; set; }

    }
}