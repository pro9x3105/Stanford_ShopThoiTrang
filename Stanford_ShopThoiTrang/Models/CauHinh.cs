namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CauHinh")]
    public partial class CauHinh
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string TenCauHinh { get; set; }

        [StringLength(100)]
        public string GiaTri { get; set; }
    }
}
