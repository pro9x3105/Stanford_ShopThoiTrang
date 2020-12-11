using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopThoiTrang.Areas.Admin.Models
{
    public class Common
    {
        /// <summary>
        /// Lưu thông tin người dùng đang đăng nhập
        /// </summary>
        public static NguoiDung UserInfo { get; set; }

        /// <summary>
        /// Lưu id của người dùng
        /// </summary>
        public static int UserId { get; set; }
    }
}