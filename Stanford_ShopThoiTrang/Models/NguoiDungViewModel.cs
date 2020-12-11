using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopThoiTrang.Models
{
    public class NguoiDungViewModel
    {
        public int Id { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public int VaiTroId { get; set; }
        public virtual VaiTro VaiTro { get; set; }
    }
}