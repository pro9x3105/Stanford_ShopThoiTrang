namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string TenMenu { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        public int? ParentId { get; set; }

        public bool? DaDuyet { get; set; }
    }
}
