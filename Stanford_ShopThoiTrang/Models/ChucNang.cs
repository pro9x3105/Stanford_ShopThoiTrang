namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChucNang")]
    public partial class ChucNang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChucNang()
        {
            PhanQuyens = new HashSet<PhanQuyen>();
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string TenChucNang { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        [StringLength(100)]
        public string TenForm { get; set; }

        public int? Module { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }
    }
}
