namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuongTrinhChiTiet")]
    public partial class ChuongTrinhChiTiet
    {
        public int Id { get; set; }

        public int? ChuongTrinhId { get; set; }

        public int? SanPhamId { get; set; }

        public double? GiamGia { get; set; }

        public virtual ChuongTrinhUuDai ChuongTrinhUuDai { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
