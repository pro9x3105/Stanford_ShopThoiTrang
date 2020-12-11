using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    public class HangSanXuatController : BaseController
    {
        // GET: Admin/HangSanXuat
        public ActionResult DanhSach()
        {
            var lstHang = DataProvider.ShopEntities.Hangs.ToList();
            return View(lstHang);
        }
        public JsonResult ThemHangJson(string tenHang, string moTa, int id)
        {
            //Khai báo 1 đối tượng
            Hang objHang = new Hang();
            //Gán giá trị
            objHang.TenHang = tenHang;
            objHang.MoTa = moTa;
            if (id > 0)//TH sửa
            {
                objHang.Id = id;
                //Lấy đối tượng cũ
                Hang objHangOld = DataProvider.ShopEntities.Hangs.Where(p => p.Id == id).First();

                DataProvider.ShopEntities.Entry(objHangOld).CurrentValues.SetValues(objHang);
            }
            else
            {
                //Thực hiện thêm và lưu sự thay đổi
                DataProvider.ShopEntities.Hangs.Add(objHang);
            }

            DataProvider.ShopEntities.SaveChanges();
            return Json(objHang, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Hàm lấy thông tin chi tiết theo id và trả dạng json
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ChiTietHangJson(int id)
        {
            Hang objHang = DataProvider.ShopEntities.Hangs.Where(p => p.Id == id).First();

            return Json(objHang, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult Xoa(int id)
        {
            Hang objHang = DataProvider.ShopEntities.Hangs.Where(p => p.Id == id).First();

            if (objHang != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.Hangs.Remove(objHang);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }

            return Json(objHang, JsonRequestBehavior.AllowGet);
        }
    }


}