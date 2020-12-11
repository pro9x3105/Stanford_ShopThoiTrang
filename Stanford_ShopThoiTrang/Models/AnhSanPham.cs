namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnhSanPham")]
    public partial class AnhSanPham
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string TenAnh { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        public int? SapXep { get; set; }

        public int? SanPhamId { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
