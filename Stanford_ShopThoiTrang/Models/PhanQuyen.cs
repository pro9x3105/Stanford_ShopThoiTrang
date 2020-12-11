namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhanQuyen")]
    public partial class PhanQuyen
    {
        public int Id { get; set; }

        public int? ChucNangId { get; set; }

        public int? VaiTroId { get; set; }

        public bool? Xem { get; set; }

        public bool? Them { get; set; }

        public bool? Sua { get; set; }

        public bool? Xoa { get; set; }

        public virtual ChucNang ChucNang { get; set; }

        public virtual VaiTro VaiTro { get; set; }
    }
}
