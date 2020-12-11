using Stanford_ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopThoiTrang.Areas.Admin.Controllers
{
    public class CTKhuyenMaiController :BaseController
    {
        // GET: Admin/CTKhuyenMai
        public ActionResult DanhSach()
        {
            var lstUuDai = DataProvider.ShopEntities.ChuongTrinhUuDais.ToList();
            return View(lstUuDai);
        }
        public JsonResult ThemChuongTrinhJson()
        {
            var data = Request.Form;
            //Khai báo 1 đối tượng
            ChuongTrinhUuDai objUuDai = new ChuongTrinhUuDai();
            //Gán giá trị
            objUuDai.TenChuongTrinh = data["tenCT"];
            objUuDai.MoTa = data["moTa"];
            objUuDai.NgayTao = DateTime.Now;
            objUuDai.TuNgay = Convert.ToDateTime(data["BatDau"]);
            objUuDai.DenNgay = Convert.ToDateTime(data["KetThuc"]);            
            objUuDai.DaDuyet = Convert.ToBoolean(data["daduyet"]);
            var id = Convert.ToInt32(data["id"]);
            if (id > 0)//TH sửa
            {
                objUuDai.Id = id;
                //Lấy đối tượng cũ
                ChuongTrinhUuDai objChuongTrinhld = DataProvider.ShopEntities.ChuongTrinhUuDais.Where(p => p.Id == id).First();
                DataProvider.ShopEntities.Entry(objChuongTrinhld).CurrentValues.SetValues(objUuDai);
                DataProvider.ShopEntities.SaveChanges();
                // nhật ký sửa 
                // 1.Thêm mới 2 Sửa 3.Xóa
                NhatKyCommon.Them("Người dùng thực hiện chức năng Sửa Chương trình khuyến mãi", "Sửa");
            }
            else
            {
                //Thực hiện thêm và lưu sự thay đổi
                DataProvider.ShopEntities.ChuongTrinhUuDais.Add(objUuDai);
                DataProvider.ShopEntities.SaveChanges();
                // Nhật ký thêm 
                // 1.Thêm mới 2 Sửa 3.Xóa
                NhatKyCommon.Them("Người dùng thực hiện chức năng Thêm mới Chương trình khuyến mãi", "Thêm Mới");
            }

           
            return Json(objUuDai, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChiTietChuongTrinhJson(int id)
        {
            ChuongTrinhUuDai objChuongTrinh = DataProvider.ShopEntities.ChuongTrinhUuDais.Where(p => p.Id == id).First();
             ChuontrinhUuDaiViewModel objChuongTrinhView = new ChuontrinhUuDaiViewModel();
            if (objChuongTrinh != null)
            {
                objChuongTrinhView.Id = objChuongTrinh.Id;
                objChuongTrinhView.TenChuongTrinh = objChuongTrinh.TenChuongTrinh;
                objChuongTrinhView.MoTa = objChuongTrinh.MoTa;                           
                objChuongTrinhView.TuNgay = objChuongTrinh.TuNgay;
                objChuongTrinhView.DenNgay = objChuongTrinh.DenNgay;
                objChuongTrinhView.DaDuyet = objChuongTrinh.DaDuyet;
            }
            return Json(objChuongTrinhView, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xoa(int id)
        {
            ChuongTrinhUuDai objChuongTrinh = DataProvider.ShopEntities.ChuongTrinhUuDais.Where(p => p.Id == id).First();

            if (objChuongTrinh != null)
            {
                //Thực hiện xóa
                DataProvider.ShopEntities.ChuongTrinhUuDais.Remove(objChuongTrinh);
                //Lưu thay đổi
                DataProvider.ShopEntities.SaveChanges();

            }
            // Nhật ký xóa
            // 1.Thêm mới 2 Sửa 3.Xóa
            NhatKyCommon.Them("Người dùng thực hiện chức năng Xóa Chương trình khuyến mãi chi tiết", "Xóa");
            return Json(objChuongTrinh, JsonRequestBehavior.AllowGet);
        }
    }

   
}

