
using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    public class ChucNangController : Controller
    {
        // GET: Admin/ChucNang
        public ActionResult DanhSach()
        {
            var lstChucNang = DataProvider.ShopEntities.ChucNangs.ToList();
            return View(lstChucNang);
        }
        public JsonResult ThemChucNangJson()
        {
            var data = Request.Form;
            //Khai báo 1 đối tượng
            ChucNang objChucNang = new ChucNang();
            //Gán giá trị
            objChucNang.TenChucNang = data["tenCN"];
            objChucNang.MoTa = data["moTa"];
            objChucNang.TenForm = data["tenForm"];
            objChucNang.Module = Convert.ToInt32(data["module"]);
            var id = Convert.ToInt32(data["id"]);
            if (id > 0)//TH sửa
            {
                objChucNang.Id = id;
                //Lấy đối tượng cũ
                ChucNang objChuDeOld = DataProvider.ShopEntities.ChucNangs.Where(p => p.Id == id).First();
                DataProvider.ShopEntities.Entry(objChuDeOld).CurrentValues.SetValues(objChucNang);
                DataProvider.ShopEntities.SaveChanges();

                // Nhật ký sửa
                // 1.Thêm mới 2.Sửa 3.Xóa 
                NhatKyCommon.Them("nguoi dung thực hiện hành động Sửa Chức năng","Sửa");

            }
            else
            {
                //Thực hiện thêm và lưu sự thay đổi
                DataProvider.ShopEntities.ChucNangs.Add(objChucNang);
                DataProvider.ShopEntities.SaveChanges();

                // Nhật ký thêm 
                // 1.Thêm mới 2.Sửa 3.Xóa 
                NhatKyCommon.Them("Người dùng thực hiện hành động Thêm mới chức năng","Thêm mới");
            }
            return Json(objChucNang, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChiTietChucNangJson(int id)
        {
            ChucNang objChucNang = DataProvider.ShopEntities.ChucNangs.Where(p => p.Id == id).First();
            return Json(objChucNang, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xoa(int id)
        {
            ChucNang objChucNang = DataProvider.ShopEntities.ChucNangs.Where(p => p.Id == id).First();

            if (objChucNang != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.ChucNangs.Remove(objChucNang);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }
            // Nhât ký xóa
            // 1.Thêm mới 2.Sửa 3.Xóa 
            NhatKyCommon.Them("Người dùng thực hiện hành động Xóa chức năng","Xóa");
            return Json(objChucNang, JsonRequestBehavior.AllowGet);
        }
    }
}