using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Stanford_ShopThoiTrang.Models
{
    public class DataProvider
    {
        private static ShopThoiTrangModel _ShopEntities = null;
        public static ShopThoiTrangModel ShopEntities
        {
            get
            {
                if (_ShopEntities == null)
                {
                    _ShopEntities = new ShopThoiTrangModel();
                }
                return _ShopEntities;
            }
        }
    }
}