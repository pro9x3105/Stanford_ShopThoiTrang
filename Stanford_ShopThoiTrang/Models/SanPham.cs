namespace Stanford_ShopThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            AnhSanPhams = new HashSet<AnhSanPham>();
            ChuongTrinhChiTiets = new HashSet<ChuongTrinhChiTiet>();
            DanhGiaSanPhams = new HashSet<DanhGiaSanPham>();
            HoaDonChiTiets = new HashSet<HoaDonChiTiet>();
        }

        public int Id { get; set; }

        [StringLength(15)]
        public string MaSanPham { get; set; }

        [StringLength(250)]
      
       
        public string TenSanPham { get; set; }

        [StringLength(1000)]
    
        public string MoTa { get; set; }

        [Column(TypeName = "ntext")]
        
        public string NoiDung { get; set; }

        [StringLength(100)]
        
        public string AnhSanPham { get; set; }
        
        public double? Gia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCapNhat { get; set; }

        public bool? DaDuyet { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDuyet { get; set; }

        [StringLength(150)]
      
        public string KichCo { get; set; }

        [StringLength(150)]
      
        public string Mau { get; set; }
       
        public int? SoLuong { get; set; }

        public double? Rating { get; set; }
       
        public int? TrangThai { get; set; }

        [StringLength(1000)]
     
        public string Tags { get; set; }
      
        public int? ChuDeId { get; set; }
        
        public int? HangId { get; set; }
       
        public int? LoaiId { get; set; }
       
        [StringLength(10)]
        public string XuatXuId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnhSanPham> AnhSanPhams { get; set; }

        public virtual ChuDe ChuDe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuongTrinhChiTiet> ChuongTrinhChiTiets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGiaSanPham> DanhGiaSanPhams { get; set; }

        public virtual Hang Hang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }

        public virtual Loai Loai { get; set; }

        public virtual QuocGia QuocGia { get; set; }
    }
}
