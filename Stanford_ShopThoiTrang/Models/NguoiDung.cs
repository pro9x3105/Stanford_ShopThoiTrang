namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            DanhGiaSanPhams = new HashSet<DanhGiaSanPham>();
            NhatKies = new HashSet<NhatKy>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string TaiKhoan { get; set; }

        [StringLength(150)]
        public string MatKhau { get; set; }

        [StringLength(30)]
        public string HoTen { get; set; }

        [StringLength(20)]
        public string DienThoai { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(250)]
        public string DiaChi { get; set; }

        public int VaiTroId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGiaSanPham> DanhGiaSanPhams { get; set; }

        public virtual VaiTro VaiTro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhatKy> NhatKies { get; set; }
    }
}
