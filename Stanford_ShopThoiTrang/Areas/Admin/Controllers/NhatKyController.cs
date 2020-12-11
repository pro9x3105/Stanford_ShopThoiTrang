
using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    public class NhatKyController : Controller
    {
        public ActionResult DanhSach()
        {
            var lstNhatKy = DataProvider.ShopEntities.NhatKies.ToList();
            return View(lstNhatKy);
        }

        public JsonResult Xoa(int id)
        {
            NhatKy objNhatKy = DataProvider.ShopEntities.NhatKies.Where(p => p.Id == id).First();

            if (objNhatKy != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.NhatKies.Remove(objNhatKy);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }
            // Nhât ký xóa
                
            return Json(objNhatKy, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int[] Id)
        {    
               for(int i =0;i< Id.Count(); i++)
                {
                NhatKy objNhatKy = DataProvider.ShopEntities.NhatKies.Find(Id[i]);
                DataProvider.ShopEntities.NhatKies.Remove(objNhatKy);
                }
                DataProvider.ShopEntities.SaveChanges();
                return Json("Tất cả các nhật ký đã được xóa");    
        }
    }
}
