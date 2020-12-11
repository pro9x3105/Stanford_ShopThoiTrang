using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    public class SlideController : Controller
    {
        // GET: Admin/Slide
        public ActionResult DanhSach()
        {
            IQueryable<Slide> lstSlide = DataProvider.ShopEntities.Slides;
            return View(lstSlide.ToList());
        }
        public ActionResult ThemSlide()
        {
            return View();
        }
        public ActionResult AddSlide(Slide objSlide, HttpPostedFileBase fAnh)
        {
            
            objSlide.TenSlide = Request["TenSlide"] + "";
            objSlide.MoTa = Request["MoTa"] + "";
            objSlide.DaDuyet = false;        
            objSlide.NgayTao = DateTime.Now;
           
          
            if (fAnh != null && fAnh.ContentLength > 0)
            {
                string tenAnh = Path.GetFileName(fAnh.FileName);
                fAnh.SaveAs(Server.MapPath("~/Content/Images/" + tenAnh));
                objSlide.Anh = tenAnh;
            }          
            DataProvider.ShopEntities.Slides.Add(objSlide);
            DataProvider.ShopEntities.SaveChanges();
            NhatKyCommon.Them("Người dùng thực hiện chức năng Thêm Mới Slide", "Thêm Mới");
            return RedirectToAction("DanhSach");
        }
        public JsonResult Xoa(int id)
        {
            Slide objSlide = DataProvider.ShopEntities.Slides.Where(p => p.Id == id).First();

            if (objSlide != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.Slides.Remove(objSlide);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }

            // Nhật ký xóa
            // 1.Thêm mới 2 Sửa 3.Xóa
            NhatKyCommon.Them("Người dùng thực hiện chức năng Xóa Slide", "Xóa");
            return Json(objSlide, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SuaSlide(int id)
        {
            Slide objSlide = DataProvider.ShopEntities.Slides.Find(id);
            return View(objSlide);
        }
        public ActionResult UpdateSlide(Slide objSlide, HttpPostedFileBase fAnh)
        {
            var id = Convert.ToInt32(Request["Id"] + "");

            if (fAnh != null && fAnh.ContentLength > 0)
            {
                string tenAnh = Path.GetFileName(fAnh.FileName);
                fAnh.SaveAs(Server.MapPath("~/Content/Images/" + tenAnh));
                objSlide.Anh = tenAnh;
            }
            Slide objSlideOld = DataProvider.ShopEntities.Slides.Find(objSlide.Id);
            if (fAnh == null)
            {
                objSlide.Anh = objSlideOld.Anh;
            }
            objSlide.TenSlide = Request["TenSlide"] + "";
            objSlide.MoTa = Request["Mota"] + "";
            objSlide.NgayTao = objSlideOld.NgayTao;
            objSlide.DaDuyet = objSlideOld.DaDuyet;
            DataProvider.ShopEntities.Entry<Slide>(objSlideOld).CurrentValues.SetValues(objSlide);
            DataProvider.ShopEntities.SaveChanges();
            NhatKyCommon.Them("Người dùng thực hiện chức năng Sửa Slide", "Sửa");
            return RedirectToAction("DanhSach");
        }
        public ActionResult Delete(int[] Id)
        {

            for (int i = 1; i <= Id.Count(); i++)
            {
                Slide objSlide = DataProvider.ShopEntities.Slides.Find(Id);
                DataProvider.ShopEntities.Slides.Remove(objSlide);
            }
            DataProvider.ShopEntities.SaveChanges();
            NhatKyCommon.Them("Người dùng thực hiện chức năng Xóa Tất cả các Slide", "Xóa tất cả");

            return Json("Tất cả các nhật ký đã được xóa");

        }


    }
}