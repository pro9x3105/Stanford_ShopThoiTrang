namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string TenSlide { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        [StringLength(100)]
        public string Anh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public bool? DaDuyet { get; set; }
    }
}
