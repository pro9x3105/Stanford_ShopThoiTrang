using Stanford_ShopThoiTrang.Areas.Admin.Models;
using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Models
{
    public class NhatKyCommon
    {
        public static bool Them(string noidung, string hanhdong)
        {
            try
            {
                
                
                NhatKy objNhatKy = new NhatKy();
                
                objNhatKy.UserId = Common.UserId;
                objNhatKy.NoiDung = noidung;
                objNhatKy.NgayTao = DateTime.Now;
                objNhatKy.HanhDong = hanhdong;
                objNhatKy.DiaChiIP = GetIP();
                DataProvider.ShopEntities.NhatKies.Add(objNhatKy);
                DataProvider.ShopEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private static string GetIP()
        {
            string strHostName = "";
            strHostName = Dns.GetHostName();

            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList;

            return addr[addr.Length - 1].ToString();

        }
    }
}