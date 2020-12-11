using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    public class KhachHangController : BaseController
    {
        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            return View(DataProvider.ShopEntities.KhachHangs.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KhachHang obj)
        {
            var x = "a";
            DataProvider.ShopEntities.KhachHangs.Add(obj);
            DataProvider.ShopEntities.SaveChanges();
            return View();
        }


        public ActionResult Update(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = DataProvider.ShopEntities.KhachHangs.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(KhachHang obj)
        {
            if (ModelState.IsValid)
            {
                KhachHang objid = DataProvider.ShopEntities.KhachHangs.Where(p => p.Id == obj.Id).First();

                DataProvider.ShopEntities.Entry(objid).CurrentValues.SetValues(obj);
                DataProvider.ShopEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public JsonResult GetById(int id)
        {
            var data = DataProvider.ShopEntities.KhachHangs.Find(id);
            if (data == null)
            {
                return Json(new { success = false, msg = "Không tìm thấy khách hàng" }, JsonRequestBehavior.AllowGet);

            }
            var obj = new KhachHang
            {
                Id = data.Id,
                HoTen = data.HoTen,
                DienThoai = data.DienThoai,
                Email = data.Email,
                DiaChi = data.DiaChi,
                LoaiKhachId = data.LoaiKhachId
            };
            return Json(new { success = true, data = obj }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Remove(int id)
        {
            var obj = DataProvider.ShopEntities.KhachHangs.Find(id);

            if (obj != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.KhachHangs.Remove(obj);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }

            return Json(new { success = true, data = obj }, JsonRequestBehavior.AllowGet);


        }

        



    }
}