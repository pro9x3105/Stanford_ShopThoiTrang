using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : BaseController
    {
        // GET: Admin/Loai
        public ActionResult DanhSach()
        {
            return View(DataProvider.ShopEntities.Loais.ToList());
        }
        public JsonResult GetById(int id)
        {
            var data = DataProvider.ShopEntities.Loais.Find(id);
            if (data == null)
            {
                return Json(new { success = false, msg = "Không tìm thấy loại sản phẩm" }, JsonRequestBehavior.AllowGet);

            }
            var obj = new Loai
            {
                Id = data.Id,
                TenLoai = data.TenLoai
            };
            return Json(new { success = true, data = obj }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Remove(int id)
        {
            var obj = DataProvider.ShopEntities.Loais.Find(id);

            if (obj != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.Loais.Remove(obj);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }

            return Json(new { success = true, data = obj }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult Create(int id, string TenLoai)
        {
            //Khai báo 1 đối tượng
            Loai obj = new Loai();
            //Gán giá trị
            obj.Id = id;
            obj.TenLoai = TenLoai;
            if (id > 0)//TH sửa
            {
                obj.Id = id;
                //Lấy đối tượng cũ
                Loai objid = DataProvider.ShopEntities.Loais.Where(p => p.Id == id).First();

                DataProvider.ShopEntities.Entry(objid).CurrentValues.SetValues(obj);
            }
            else
            {
                //Thực hiện thêm và lưu sự thay đổi
                DataProvider.ShopEntities.Loais.Add(obj);
            }

            DataProvider.ShopEntities.SaveChanges();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}