namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhGiaSanPham")]
    public partial class DanhGiaSanPham
    {
        public int Id { get; set; }

        [StringLength(1000)]
        public string NoiDung { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public double? Rating { get; set; }

        public bool? DaDuyet { get; set; }

        public int? UserId { get; set; }

        public int? SanPhamId { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
