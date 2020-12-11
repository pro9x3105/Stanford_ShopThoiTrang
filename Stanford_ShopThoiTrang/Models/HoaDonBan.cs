namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonBan")]
    public partial class HoaDonBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDonBan()
        {
            HoaDonChiTiets = new HashSet<HoaDonChiTiet>();
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string TenHoaDon { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBan { get; set; }

        public int? TrangThai { get; set; }

        public bool? DaThanhToan { get; set; }

        public int? KhachHangId { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }
    }
}
