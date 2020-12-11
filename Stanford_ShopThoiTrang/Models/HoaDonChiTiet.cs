namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonChiTiet")]
    public partial class HoaDonChiTiet
    {
        public int Id { get; set; }

        public int? SanPhamId { get; set; }

        public int? HoaDonId { get; set; }

        public int? SoLuong { get; set; }

        public double? Gia { get; set; }

        public virtual HoaDonBan HoaDonBan { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
