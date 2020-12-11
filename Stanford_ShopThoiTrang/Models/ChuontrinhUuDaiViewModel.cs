using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stanford_ShopThoiTrang.Models
{
    public class ChuontrinhUuDaiViewModel
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string TenChuongTrinh { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TuNgay { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DenNgay { get; set; }

        public bool? DaDuyet { get; set; }
    }
}