namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhatKy")]
    public partial class NhatKy
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string NoiDung { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? UserId { get; set; }

        [StringLength(20)]
        public string DiaChiIP { get; set; }
        [StringLength(50)]
        public string HanhDong { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
