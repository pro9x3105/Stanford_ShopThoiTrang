using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    public class ChuDeController : BaseController
    {
        // GET: Admin/ChuDe
        public ActionResult DanhSach()
        {
            var lstChuDe = DataProvider.ShopEntities.ChuDes.ToList();
            return View(lstChuDe);
        }

        /// <summary>
        /// Hàm thêm mới chủ đề bằng ajax
        /// Author          DateCreate      Comments
        /// DangBQ          01/07/2020  Tạo mới
        /// </summary>
        /// <param name="tenCD"></param>
        /// <returns></returns>
        public JsonResult ThemChuDeJson(string tenCD, string moTa,string anh, int id)
        {
            //Khai báo 1 đối tượng
            ChuDe objChuDe = new ChuDe();
            //Gán giá trị
            objChuDe.TenChuDe = tenCD;
            objChuDe.MoTa = moTa;
            objChuDe.AnhChuDe = anh;
            if (id > 0)//TH sửa
            {
                objChuDe.Id = id;
                //Lấy đối tượng cũ
                ChuDe objChuDeOld = DataProvider.ShopEntities.ChuDes.Where(p => p.Id == id).First();

                DataProvider.ShopEntities.Entry(objChuDeOld).CurrentValues.SetValues(objChuDe);
            }
            else
            {
                if(objChuDe.TenChuDe=="")
                {
                    return Json(new { success = false,coTenChuDe=0, msg = "Tên chủ đề không được trống" }, JsonRequestBehavior.AllowGet);
                }
                if(BatLoiTrungTenChuDe(objChuDe.TenChuDe))
                {
                    return Json(new { success = false,coTrungTenChuDe=0, msg = "Tên chủ đề đã tồn tại" }, JsonRequestBehavior.AllowGet);
                }
                if(objChuDe.AnhChuDe=="")
                {
                    return Json(new { success = false,coAnhChuDe=0, msg = "Ảnh chủ đề không được trống" }, JsonRequestBehavior.AllowGet);
                }
                //Thực hiện thêm và lưu sự thay đổi
                DataProvider.ShopEntities.ChuDes.Add(objChuDe);
            }

            DataProvider.ShopEntities.SaveChanges();
            return Json(new {success=true,data=objChuDe }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Hàm lấy thông tin chi tiết theo id và trả dạng json
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ChiTietChuDeJson(int id)
        {
            ChuDe objChuDe = DataProvider.ShopEntities.ChuDes.Where(p => p.Id == id).First();
            object[] parsChuDe = new object[4];
            parsChuDe[0] = objChuDe.Id;
            parsChuDe[1] = objChuDe.TenChuDe;
            parsChuDe[2] = objChuDe.MoTa;
            parsChuDe[3] = objChuDe.AnhChuDe;
            return Json(parsChuDe, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xóa chủ đề
        /// </summary>
        /// <param name="id">Id của chủ đề cần xóa</param>
        /// <returns></returns>
        public JsonResult Xoa(int id)
        {
            ChuDe objChuDe = DataProvider.ShopEntities.ChuDes.Where(p => p.Id == id).First();

            if (objChuDe != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.ChuDes.Remove(objChuDe);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();
            }

            return Json(objChuDe, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UploadAnh(HttpPostedFileBase file)
        {                                 
            if(file!=null&&file.ContentLength>0)
            {
                file.SaveAs(Server.MapPath("~/Content/Images/" + file.FileName));
            }            
            return Json(file.FileName,JsonRequestBehavior.AllowGet);
        }
        private bool BatLoiTrungTenChuDe(string TenChuDe)
        {
            bool KetQua = false;
            foreach (ChuDe item in DataProvider.ShopEntities.ChuDes.ToList())
            {                
                if((item.TenChuDe+"")==TenChuDe)
                {
                    KetQua = true;
                }
            }
            return KetQua;
        }
    }
}