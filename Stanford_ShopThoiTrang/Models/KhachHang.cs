namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            HoaDonBans = new HashSet<HoaDonBan>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string HoTen { get; set; }

        [StringLength(20)]
        public string DienThoai { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(250)]
        public string DiaChi { get; set; }

        public int? LoaiKhachId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonBan> HoaDonBans { get; set; }

        public virtual LoaiKhachHang LoaiKhachHang { get; set; }
    }
}
